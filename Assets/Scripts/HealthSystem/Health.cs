using UnityEngine;

namespace HealthSystem
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float startingHealth;
        public float currentHealth { get; private set; }
        private bool dead;
        private Animator animator;

        private void Awake()
        {
            currentHealth = startingHealth;
            animator = GetComponent<Animator>();
        }

        public void TakeDamage(float damage)
        {
            currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

            if (currentHealth > 0)
            {
                animator.SetTrigger("hurt");
            }
            else
            {
                if (!dead)
                {
                    animator.SetTrigger("die");
                    GetComponent<Player.Player>().enabled = false;
                    dead = true;
                }
            }
        }
    }
}