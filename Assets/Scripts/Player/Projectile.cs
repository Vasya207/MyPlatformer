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
            }

            movementSpeed = Constants.ProjectileSpeed * Time.deltaTime * direction;
            transform.Translate(movementSpeed, 0, 0);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Platforms"))
            {
                movementSpeed = 0;
            }
        
            else if (collision.CompareTag("Enemy"))
            {
                Debug.Log("ENEMY");
                collision.GetComponent<Enemy>().TakeDamage(Constants.ProjectileDamageAmount);
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