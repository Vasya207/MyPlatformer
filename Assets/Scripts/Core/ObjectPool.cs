using System;
using System.Collections;
using System.Security.Cryptography;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

namespace Core
{
    public class ObjectPool : Singleton<ObjectPool>
    {
        [SerializeField] private Projectile objectToPool;
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
            obj.gameObject.SetActive(true);
        }
        
        private void ActionOnRelease(Projectile obj)
        {
            obj.gameObject.SetActive(false);
        }
    
        private void ActionOnDestroy(Projectile obj)
        {
            Destroy(obj.gameObject);
        }
    
        private void Spawn(float dir)
        {
            Debug.Log("Spawning");
            var obj = pooledObjects.Get();
            obj.transform.position = spawnPosition.transform.position;
            obj.SetDirection(dir);
        }
    
        private void DeactivateObject(Projectile obj)
        {
            pooledObjects.Release(obj);
        }
    }
}
