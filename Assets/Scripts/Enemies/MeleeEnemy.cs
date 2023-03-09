using Core;
using UnityEngine;

namespace Enemies
{
    public class MeleeEnemy : MonoBehaviour
    {
        [Header("Attack Parameters")] 
        [SerializeField] private float attackCooldown;

        [SerializeField] private float range;
        [SerializeField] private int hitDamage;
        [SerializeField] private float collisionDamage;

        [Header("Health Components")] 
        [SerializeField] private float startingHealth;

        [SerializeField] public int rewardForKill;

        [Header("Collider Parameters")] 
        [SerializeField] private float colliderDistance;

        [SerializeField] private BoxCollider2D boxCollider;

        [Header("Player Layer")] 
        [SerializeField] private LayerMask playerLayer;

        [Header("SFX")] 
        [SerializeField] private AudioClip attackSound;
        [SerializeField] private AudioClip hurtSound;
        [SerializeField] private AudioClip deathSound;

        public bool dead { get; private set; }
        private float cooldownTimer = Mathf.Infinity;
        private float currentHealth;

        private ParticleSystem bloodParticles;

        private EnemyAnimationController enemyAnimationController;
        private EnemiePatrol enemyPatrol;
        private Player.Player player;

        private void Awake()
        {
            currentHealth = startingHealth;
            enemyAnimationController = GetComponent<EnemyAnimationController>();
            enemyPatrol = GetComponentInParent<EnemiePatrol>();
            bloodParticles = GetComponentInChildren<ParticleSystem>();
        }

        private void Update()
        {
            cooldownTimer += Time.deltaTime;

            if (PlayerInSight() && player.currentHealth > 0)
                if (cooldownTimer >= attackCooldown)
                {
                    cooldownTimer = 0f;
                    enemyAnimationController.SetAction(EnemyAnimationController.EnemyState.MeleeAttack);
                    SoundManager.instance.PlaySound(attackSound);
                }

            if (enemyPatrol != null) enemyPatrol.enabled = !PlayerInSight();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")) 
                collision.GetComponent<Player.Player>().TakeDamage(collisionDamage);
        }

        private bool PlayerInSight()
        {
            var hit = Physics2D.BoxCast(
                boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
                0f, Vector2.left, 0f, playerLayer);

            if (hit.collider != null) player = hit.transform.GetComponent<Player.Player>();

            return hit.collider != null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(
                boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        }

        private void DamagePlayer()
        {
            if (PlayerInSight())
            {
                if (!player.dead)
                {
                    player.TakeDamage(hitDamage);
                }

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
            bloodParticles.Play();

            if (currentHealth > 0)
            {
                enemyAnimationController.SetAction(EnemyAnimationController.EnemyState.ReceiveDamage);
                SoundManager.instance.PlaySound(hurtSound);
            }
            else
            {
                if (!dead)
                {
                    enemyAnimationController.SetAction(EnemyAnimationController.EnemyState.Death);

                    boxCollider.enabled = false;
                    GetComponent<MeleeEnemy>().enabled = false;
                    GetComponentInParent<EnemiePatrol>().enabled = false;
                    transform.position = new Vector2(transform.position.x, transform.position.y - 0.3f);
                    dead = true;

                    SoundManager.instance.PlaySound(deathSound);

                    FindObjectOfType<GameSession>().AddToScore(rewardForKill);
                }
            }
        }
    }
}