using Core;
using Helpers;
using UnityEngine;

namespace Coin
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private AudioClip collectSound;

        private bool wasCollected;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player") && !wasCollected)
            {
                SoundManager.Instance.PlaySound(collectSound);
                GameSession.Instance.AddToScore(Constants.CoinValue);
                wasCollected = true;
                Destroy(gameObject);
            }
        }
    }
}
