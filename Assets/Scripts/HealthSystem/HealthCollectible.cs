
using Core;
using UnityEngine;

namespace HealthSystem
{
    public class HealthCollectible : MonoBehaviour
    {
        [SerializeField] private float healthValue;
        [SerializeField] private AudioClip collectSound;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                var player = collision.GetComponent<Player.Player>();
                if (player != null)
                {
                    if (player.CurrentHealth >= 3) return;
                    player.AddHealth(healthValue);
                    SoundManager.Instance.PlaySound(collectSound);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}