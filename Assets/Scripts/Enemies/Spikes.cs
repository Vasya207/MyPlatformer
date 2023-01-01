using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] int damageAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(damageAmount);
        }
    }
}
