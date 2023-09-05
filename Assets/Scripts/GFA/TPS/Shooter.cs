using System;
using System.Collections;
using System.Collections.Generic;
using GFA.TPS.Movement;
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
        private GameObject _defaultProjectilePrefab;

        private WeaponGraphics _activeWeaponGraphics;

        [SerializeField]
        private Transform _weaponContainer;

        private IObjectPool<GameObject> _projectilePool;

        private void Awake()
        {
            _projectilePool = new ObjectPool<GameObject>(CreatePoolProjectile, OnGetPoolProjectile, OnReleasePoolObject, OnDestroyFromPool, true, 40);
        }

        private GameObject CreatePoolProjectile()
        {
            var projectileToInstantiate = _defaultProjectilePrefab;

            if (_weapon.ProjectilePrefab)
            {
                projectileToInstantiate = _weapon.ProjectilePrefab;
            }

            var inst = Instantiate(projectileToInstantiate, _activeWeaponGraphics.ShootTransform.position, _activeWeaponGraphics.ShootTransform.rotation);

            if (inst.TryGetComponent<ProjectileMovement>(out var projectileMovement))
            {
                projectileMovement.DestroyRequested += () => { _projectilePool.Release(inst); };
            }

            return inst;
        }

        private void OnDestroyFromPool(GameObject obj)
        {
            Destroy(obj);
        }

        private void OnReleasePoolObject(GameObject obj)
        {
            if (obj.TryGetComponent<ProjectileMovement>(out var movement))
            {
                movement.enabled = false;
            }
        }

        private void OnGetPoolProjectile(GameObject obj)
        {
            obj.SetActive(true);
            if (obj.TryGetComponent<ProjectileMovement>(out var movement))
            {
                movement.enabled = true;

                movement.ResetSpawnTime();
            }
        }

        private IEnumerator ClearTrailRenderedDelayed(TrailRenderer trail)
        {
            trail.emitting = false;
            yield return new WaitForEndOfFrame();
            trail.Clear();
            trail.emitting = true;
        }

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

            if (_weapon)
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

            var inst = _projectilePool.Get();
            inst.transform.position = _activeWeaponGraphics.ShootTransform.position;
            inst.transform.rotation = _activeWeaponGraphics.ShootTransform.rotation;

            var trail = inst.GetComponentInChildren<TrailRenderer>();
            if (trail)
            {
                trail.Clear();
                //StartCoroutine(ClearTrailRenderedDelayed(trail));

            }
            if (inst.TryGetComponent<ProjectileDamage>(out var projectileDamage))
            {
                projectileDamage.Damage = _weapon.BaseDamage;
            }
            var rand = Random.value;
            var maxAngle = 15 - 15 * Mathf.Max(_weapon.Accuracy - _recoilValue - 0);
            //var minAngle = 60 - 60 * _accuracy;
            var randomAngle = Mathf.Lerp(-maxAngle, maxAngle, rand);

            var forward = inst.transform.forward;
            forward = Quaternion.Euler(0, randomAngle, 0) * forward;

            inst.transform.forward = forward;

            _lastShootTime = Time.time;
            _recoilValue += _weapon.Recoil;

            _activeWeaponGraphics.OnShoot();
        }

        private void Update()
        {
            _recoilValue = Mathf.MoveTowards(_recoilValue, 0, _weapon.RecoilFade * Time.deltaTime);
        }

    }
}