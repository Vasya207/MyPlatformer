using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] float attackCooldown;
    [SerializeField] float range;
    [SerializeField] int damage;

    [Header ("Collider Parameters")]
    [SerializeField] float colliderDistance;
    [SerializeField] BoxCollider2D boxCollider;

    [Header ("Player Layer")]
    [SerializeField] LayerMask playerLayer;

    private float cooldownTimer = Mathf.Infinity;

    Animator animator;
    EnemiePatrol enemyPatrol;
    Player player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemiePatrol>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0f;
                animator.SetTrigger("meleeAttack");
            }
        }

        if(enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
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
            //Damage Player
            player.TakeDamage(damage);
        }
    }
}
