
using Core;
using Helpers;
using UnityEngine;

namespace HealthSystem
{
    public class HealthCollectible : MonoBehaviour
    {
        [SerializeField] private AudioClip collectSound;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Signals.OnHealthCollect.Invoke(Constants.HealthValue);
                SoundManager.Instance.PlaySound(collectSound);
                gameObject.SetActive(false);
            }
        }
    }
}