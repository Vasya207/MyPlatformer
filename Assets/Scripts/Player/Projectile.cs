using System;
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
        private float movementSpeed;
        
        private void Update()
        {
            lifetime += Time.deltaTime;

            if (lifetime > Constants.ProjectileLifetimeValue)
            {
                gameObject.SetActive(false);
                //Signals.OnDeactivateProjectile.Invoke(this);
            }

            if (!hit)
            {
                movementSpeed = Constants.ProjectileSpeed * Time.deltaTime * direction;
                transform.Translate(movementSpeed, 0, 0);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Platforms"))
            {
                hit = true;
            }
        
            else if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<Enemy>().TakeDamage(Constants.ProjectileDamageAmount);
                //Signals.OnDeactivateProjectile.Invoke(this);
                gameObject.SetActive(false);
            }
        }

        public void SetDirection(float dir)
        {
            direction = dir;

            var localScaleX = transform.localScale.x;
            if (Mathf.Sign(localScaleX) != dir)
            {
                localScaleX = -localScaleX;
            }

            transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
        }
    }
}