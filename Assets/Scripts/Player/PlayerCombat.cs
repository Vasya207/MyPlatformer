using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack Components")]
    [SerializeField] int damageAmount = 2;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] float attackCooldown = 1f;
    [SerializeField] Transform attackPoint;

    [Header("Layers")]
    [SerializeField] LayerMask enemyLayer;

    [Header("Audio Components")]
    [SerializeField] AudioClip hitAudio;
    [SerializeField] AudioClip swordWavingAudio;

    float cooldownTimer = Mathf.Infinity;
    Animator myAnimator;
    Player player;
    
    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    void OnHit(InputValue inputValue)
    {
        if (cooldownTimer > attackCooldown)
        {
            cooldownTimer = Mathf.Epsilon;
            myAnimator.SetTrigger("attack");
        }
    }

    void Attack()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        SoundManager.instance.PlaySound(swordWavingAudio);

        foreach (Collider2D hitObject in hitObjects)
        {
            if(hitObject.tag == "Enemy")
            {
                SoundManager.instance.PlaySound(hitAudio);
                hitObject.GetComponent<MeleeEnemy>().TakeDamage(damageAmount);
            }
            else if(hitObject.tag == "Chest")
            {
                hitObject.GetComponent<ChestLogic>().OpenTheChest();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public bool AttackFinished()
    {
        return cooldownTimer > 1.2f;
    }
}
