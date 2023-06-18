using System;
using Player;
using UnityEngine.Events;

public static class Signals
{
    public static UnityEvent OnLevelFinished = new();
    public static UnityEvent<float> OnDamagePlayer = new();
    public static UnityEvent<float> OnDamageEnemy = new();
    public static UnityEvent<Projectile> OnDeactivateProjectile = new();
    public static UnityEvent<float> OnSpawnProjectile = new();
    public static Func<float, bool> OnHealthCollectFunc;
}
