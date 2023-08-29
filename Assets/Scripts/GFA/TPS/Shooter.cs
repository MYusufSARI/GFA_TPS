using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace GFA.TPS
{

    public class Shooter : MonoBehaviour
    {
        [SerializeField]
        private float _fireRate = 0.5f;

        [SerializeField]
        private float _accuracy = 1f;

        private float _lastShootTime;

        public bool CanShoot => Time.time > _lastShootTime + _fireRate;

        [SerializeField]
        private GameObject _projectilePrefab;

        [SerializeField]
        private Transform _shootTransform;

        private IObjectPool<GameObject> _projectilePool;

        private void Awake()
        {
            //      _projectilePool = new ObjectPool<GameObject>(CreatePoolProjectile , OnGetPoolProjectile , OnReleasePoolObject , OnDestroyFromPool , true , 40);
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
        /*
        private GameObject CreatePoolProjectile()
        {
            
        }
        */
        public void Shoot()
        {
            if (!CanShoot)
            {
                return;
            }

            var inst = Instantiate(_projectilePrefab, _shootTransform.position, _shootTransform.rotation);
            var rand = Random.value; 
            var maxAngle = 60 - 60 * _accuracy;
            //var minAngle = 60 - 60 * _accuracy;
            var randomAngle = Mathf.Lerp(-maxAngle,maxAngle , rand);

            var forward = inst.transform.forward;
            forward =  Quaternion.Euler(0, randomAngle, 0) * forward;

            inst.transform.forward = forward;

            _lastShootTime = Time.time;
        }


    }
}