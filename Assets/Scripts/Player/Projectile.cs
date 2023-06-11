using Enemies;
using UnityEngine;

namespace Player
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float damageAmount = 1f;
        [SerializeField] private float lifetimeValue = 2f;

        private float direction;
        private bool hit;
        private float lifetime;

        private BoxCollider2D myCollider;

        private void Awake()
        {
            myCollider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            if (hit)
            {
                lifetime += Time.deltaTime;

                if (lifetime > lifetimeValue)
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                var movementSpeed = speed * Time.deltaTime * direction;
                transform.Translate(movementSpeed, 0, 0);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Platforms"))
            {
                hit = true;
                myCollider.enabled = false;
            }

            else if (collision.CompareTag("Enemy"))
            {
                myCollider.enabled = false;
                gameObject.SetActive(false);
                collision.GetComponent<Enemy>().TakeDamage(damageAmount);
                //Signals.OnDamageEnemy.Invoke(damageAmount);
            }
        }

        public void SetDirection(float dir)
        {
            lifetime = 0;
            direction = dir;
            gameObject.SetActive(true);
            hit = false;
            myCollider.enabled = true;

            var localScaleX = transform.localScale.x;
            if (Mathf.Sign(localScaleX) != dir) localScaleX = -localScaleX;

            transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
        }
    }
}