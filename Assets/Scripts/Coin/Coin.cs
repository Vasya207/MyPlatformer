using System;
using Core;
using UnityEngine;

namespace Coin
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] public int pointsForCoinPickup;
        [SerializeField] private AudioClip collectSound;

        private GameSession gameSession;

        private bool wasCollected;

        private void Awake()
        {
            gameSession = GameSession.Instance;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player") && !wasCollected)
            {
                SoundManager.instance.PlaySound(collectSound);
                gameSession.AddToScore(pointsForCoinPickup);
                wasCollected = true;
                Destroy(gameObject);
            }
        }
    }
}
