
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
                var res = Signals.OnHealthCollectFunc.Invoke(Constants.HealthValue);
                if (!res)
                {
                    gameObject.SetActive(false);
                }
                SoundManager.Instance.PlaySound(collectSound);
            }
        }
    }
}