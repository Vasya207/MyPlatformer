using System;
using System.Security.Cryptography;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Projectile objectToPool;
    [SerializeField] private int amountToPool = 1;
    [SerializeField] private Transform spawnPosition;
    private ObjectPool<Projectile> pooledObjects;

    private const int dafaultCapacity = 5;
    private const int maxSize = 20;

    private void Start()
    {
        pooledObjects = new ObjectPool<Projectile>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy,
            false, dafaultCapacity, maxSize);
    }

    private void OnEnable()
    {
        Signals.OnSpawnProjectile.AddListener(Spawn);
        Signals.OnDeactivateProjectile.AddListener(DeactivateObject);
    }

    private void OnDisable()
    {
        Signals.OnSpawnProjectile.RemoveListener(Spawn);
        Signals.OnDeactivateProjectile.RemoveListener(DeactivateObject);
    }

    private Projectile CreateFunc()
    {
        return Instantiate(objectToPool);
    }

    private void ActionOnGet(Projectile obj)
    {
        objectToPool.gameObject.SetActive(true);
    }
    
    private void ActionOnRelease(Projectile obj)
    {
        objectToPool.gameObject.SetActive(false);
    }
    
    private void ActionOnDestroy(Projectile obj)
    {
        Destroy(objectToPool.gameObject);
    }

    public void Spawn(float dir)
    {
        var obj = pooledObjects.Get();
        objectToPool.transform.position = spawnPosition.position;
        objectToPool.transform.rotation = spawnPosition.rotation;
        obj.SetDirection(dir);
    }

    private void DeactivateObject(Projectile obj)
    {
        pooledObjects.Release(obj);
    }

}
