using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] float attackCooldown;
    [SerializeField] float range;
    [SerializeField] int hitDamage;
    [SerializeField] float collisionDamage;

    [Header("Health Components")]
    [SerializeField] float startingHealth;

    [Header("Collider Parameters")]
    [SerializeField] float colliderDistance;
    [SerializeField] BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] LayerMask playerLayer;

    [Header("SFX")]
    [SerializeField] AudioClip attackSound;
    [SerializeField] AudioClip hurtSound;
    [SerializeField] AudioClip deathSound;

    public bool dead { get; private set; }
    private float cooldownTimer = Mathf.Infinity;
    private float currentHealth;

    Animator myAnimator;
    EnemiePatrol enemyPatrol;
    Player player;

    private void Awake()
    {
        currentHealth = startingHealth;
        myAnimator = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemiePatrol>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight() && player.currentHealth > 0)
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0f;
                myAnimator.SetTrigger("meleeAttack");
                SoundManager.instance.PlaySound(attackSound);
            }
        }

        if(enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(collisionDamage);
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0f, Vector2.left, 0f, playerLayer);

        if(hit.collider != null)
        {
            player = hit.transform.GetComponent<Player>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        //if player is still in sight
        if (PlayerInSight())
        {
            if(!player.dead)
                player.TakeDamage(hitDamage);

            else
            {
                GetComponentInParent<EnemiePatrol>().enabled = false;
                gameObject.GetComponent<MeleeEnemy>().enabled = false;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            myAnimator.SetTrigger("hurt");
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {
                myAnimator.SetTrigger("die");
                boxCollider.enabled = false;
                GetComponent<MeleeEnemy>().enabled = false;
                GetComponentInParent<EnemiePatrol>().enabled = false;
                transform.position = new Vector2(transform.position.x, transform.position.y - 0.3f);
                dead = true;

                SoundManager.instance.PlaySound(deathSound);
            }
        }
    }
}
