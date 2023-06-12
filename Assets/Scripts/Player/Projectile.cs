using Enemies;
using Helpers;
using UnityEngine;

namespace Player
{
    public class Projectile : MonoBehaviour
    {
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

                if (lifetime > Constants.ProjectileLifetimeValue)
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                var movementSpeed = Constants.ProjectileSpeed * Time.deltaTime * direction;
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
                collision.GetComponent<Enemy>().TakeDamage(Constants.ProjectileDamageAmount);
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