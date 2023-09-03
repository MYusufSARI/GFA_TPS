using System;
using System.Collections;
using System.Collections.Generic;
using GFA.TPS.WeaponSystem;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace GFA.TPS
{

    public class Shooter : MonoBehaviour
    {
        [SerializeField]
        public Weapon _weapon;

        private float _recoilValue = 0f;

        private float _lastShootTime;

        public bool CanShoot => Time.time > _lastShootTime + _weapon.FireRate;

        [SerializeField]
        private GameObject _defaulProjectilePrefab;

        [SerializeField]
        private Transform _shootTransform;

        private WeaponGraphics _activeWeaponGraphics;

        [SerializeField]
        private Transform _weaponContainer;

        private void Start()
        {
            if (_weapon)
            {
                CreateGraphics();
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            if (_activeWeaponGraphics)
            {
                ClearGraphics();
            }
            _weapon = weapon;

            if (!_weapon)
            {
                CreateGraphics();
            }
        }

        private void CreateGraphics()
        {
            if (!_weapon)
            {
                return;
            }

            var instance = Instantiate(_weapon.WeaponGraphics, _weaponContainer);
            instance.transform.localPosition = Vector3.zero;
            _activeWeaponGraphics = instance;
        }

        private void ClearGraphics()
        {
            if (!_activeWeaponGraphics)
            {
                return;
            }
            Destroy(_activeWeaponGraphics.gameObject);
            _activeWeaponGraphics = null;
        }

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
            if (!_weapon)
            {
                return;
            }

            if (!CanShoot)
            {
                return;
            }

            var projectileToInstantiate = _defaulProjectilePrefab;

            if (_weapon.ProjectilePrefab)
            {
                projectileToInstantiate = _weapon.ProjectilePrefab;
            }

            var inst = Instantiate(projectileToInstantiate, _shootTransform.position, _shootTransform.rotation);
            var rand = Random.value;
            var maxAngle = 30 - 30 * Mathf.Max(_weapon.Accuracy - _recoilValue - 0);
            //var minAngle = 60 - 60 * _accuracy;
            var randomAngle = Mathf.Lerp(-maxAngle, maxAngle, rand);

            var forward = inst.transform.forward;
            forward = Quaternion.Euler(0, randomAngle, 0) * forward;

            inst.transform.forward = forward;

            _lastShootTime = Time.time;
            _recoilValue += _weapon.Recoil;
        }

        private void Update()
        {
            _recoilValue = Mathf.MoveTowards(_recoilValue, 0, _weapon.RecoilFade * Time.deltaTime);
        }

    }
}