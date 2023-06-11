using UnityEngine.Events;

public static class Signals
{
    public static UnityEvent OnLevelFinished = new();
    public static UnityEvent<float> OnDamagePlayer = new();
    public static UnityEvent<float> OnDamageEnemy = new();
    public static UnityEvent<float> OnHealthCollect = new();
}
