using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip collectSound;
    [SerializeField] int pointsForCoinPickup = 50;

    bool wasCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !wasCollected)
        {
            SoundManager.instance.PlaySound(collectSound);
            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
            wasCollected = true;
            Destroy(gameObject);
        }
    }
}
