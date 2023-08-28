using System;
using System.Collections;
using System.Collections.Generic;
using GFA.TPS;
using GFA.TPS.Movement;
using UnityEngine;

namespace GFA.TPS
{

    public class ExplosiveBarrel : MonoBehaviour, IDamagable
    {
        [SerializeField]
        private float _health = 5;

        [SerializeField]
        private float _explosionRadius = 5;

        private float _explosionDamage = 5;

        private float _explosionForce = 50;

        [SerializeField]
        private AnimationCurve _explosionFalloff;

        public void ApplyDamage(float damage, GameObject causer = null)
        {

            _health -= damage;
            if (_health <= 0)
            {
                Explode();
            }
        }

        private void Explode()
        {
            var hits = Physics.OverlapSphere(transform.position, _explosionRadius);

            foreach (var hit in hits)
            {
                if (hit.transform == transform) continue;
                var distance = Vector3.Distance(transform.position, hit.transform.position);
                var rate = distance / _explosionRadius;
                var falloff = _explosionFalloff.Evaluate(rate);

                if (hit.transform.TryGetComponent<IDamagable>(out var damageable))
                {
                    damageable.ApplyDamage(_explosionDamage * falloff);
                }

                if (hit.transform.TryGetComponent<CharacterMovement>(out var movement))
                {
                    movement.ExternalForces += (hit.transform.position - transform.position).normalized * _explosionForce * falloff;
                }

                if (hit.attachedRigidbody)
                {
                    hit.attachedRigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, 1, ForceMode.Impulse);
                }
            }
            Destroy(gameObject);
        }



    }
}
