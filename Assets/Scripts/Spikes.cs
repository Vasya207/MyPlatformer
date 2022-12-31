using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] int damageAmount;
     
    public int TakeHealth()
    {
        return damageAmount;
    }
}
