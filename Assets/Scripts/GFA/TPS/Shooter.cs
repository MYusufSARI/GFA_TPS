using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace GFA.TPS
{

    public class Shooter : MonoBehaviour
    {
        [SerializeField]
        private float _fireRate = 0.5f;

        [SerializeField]
        private float _lastShootTime;

        public bool CanShoot { get; }

        private IObjectPool<GameObject> _projectilePool;

        private void Awake()
        {
            _projectilePool = new ObjectPool<GameObject>(CreatePoolProjectile , OnGetPoolProjectile , OnReleasePoolObject , OnDestroyFromPool , true , 40);
        }

        private void OnDestroyFromPool(GameObject @object)
        {
            throw new NotImplementedException();
        }

        private void OnReleasePoolObject(GameObject @object)
        {
            throw new NotImplementedException();
        }

        private void OnGetPoolProjectile(GameObject @object)
        {
            throw new NotImplementedException();
        }

        private GameObject CreatePoolProjectile()
        {
            
        }

        public void Shoot()
        {
            if (!CanShoot)
            {
                return;
            }
        }


    }
}