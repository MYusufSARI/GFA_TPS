using System;
using System.Collections;
using System.Collections.Generic;
using GFA.TPS.Movement;
using UnityEngine;

namespace GFA.TPS
{

    public class Ricochet : MonoBehaviour
    {
        private ProjectileMovement _projectileMovement;

        [SerializeField]
        private int _ricochetCount;

        [SerializeField]
        private bool _removeOnComplete;

        [SerializeField]
        private float _radius;

        private void Awake()
        {
            _projectileMovement = GetComponent<ProjectileMovement>();
        }


        private void OnEnable()
        {
            _projectileMovement.Impacted += OnImpacted;
        }


        private void OnDisable()
        {
            _projectileMovement.Impacted -= OnImpacted;
        }


        private void OnImpacted(RaycastHit raycastHit)
        {
            if (_ricochetCount >= 0)
            {
                if (_removeOnComplete)
                {
                    Destroy(this);
                }

                return;
            }

            var hits = Physics.OverlapSphere(raycastHit.point, _radius);

            foreach (var hit in hits)
            {
                if (hit.transform == raycastHit.transform) continue; 

                if (hit.transform.TryGetComponent<IDamagable>(out var _))
                {
                    var dir = (hit.transform.position - transform.position).normalized;
                    transform.forward = dir;
                    _ricochetCount--;
                    return;
                }
            }
        }
    }
}