using System;
using System.Collections;
using System.Collections.Generic;
using GFA.TPS;
using UnityEngine;

namespace GFA.TPS
{

    public class ExplosiveBarrel : MonoBehaviour, IDamagable
    {
        [SerializeField]
        private float _health = 5;

        [SerializeField]
        private float explosionRadius = 5;

        private float _explosionDamage = 5;

        private float _explosionForce = 50;

        [SerializeField]
        private AnimationCurve _explosionFallOff;

        public void ApplyDamage(float damage, GameObject causer = null)
        {

            _health -= damage;
            if (_health <= 0)
            {
                Explode();
            }
        }

        public void Explode()
        {
            var hits = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (var hit in hits)
            {


                var distance = Vector3.Distance(transform.position, hit.transform.position);

                var rate = distance / explosionRadius;

                var fallOff = _explosionFallOff.Evaluate(rate);

                if (hit.transform.TryGetComponent<IDamagable>(out var damagable))
                {
                    damagable.ApplyDamage(_explosionDamage + fallOff);
                }

                if (hit.attachedRigidbody)
                {
                    hit.attachedRigidbody.AddExplosionForce(_explosionForce, transform.position, explosionRadius, 1, ForceMode.Impulse);
                }

            }

            
            Destroy(gameObject);
        }
    }
}