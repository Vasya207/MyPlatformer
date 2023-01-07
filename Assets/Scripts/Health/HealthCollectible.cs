using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] float healthValue;
    [SerializeField] AudioClip collectSound;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (collision.GetComponent<Player>().currentHealth >= 3) return;
            collision.GetComponent<Player>().AddHealth(healthValue);
            SoundManager.instance.PlaySound(collectSound);
            gameObject.SetActive(false);
        }        
    }
}
