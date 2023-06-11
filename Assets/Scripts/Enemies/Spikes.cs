using UnityEngine;

namespace Enemies
{
    public class Spikes : MonoBehaviour
    {
        [SerializeField] int damageAmount;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player"))
            {
                Signals.OnDamagePlayer.Invoke(damageAmount);
            }
        }
    }
}
