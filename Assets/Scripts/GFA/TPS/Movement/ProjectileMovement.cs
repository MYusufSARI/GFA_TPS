using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFA.TPS.Movement
{

    public class ProjectileMovement : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }


        [SerializeField]
        private bool _shouldDisableOnCollision;
        public bool ShouldDisableOnColision
        {
            get => _shouldDisableOnCollision;
            set => _shouldDisableOnCollision = value;
        }


        [SerializeField]
        private bool _shouldDestroyOnCollision;
        public bool ShouldDestroyOnColision
        {
            get => _shouldDestroyOnCollision;
            set => _shouldDestroyOnCollision = value;
        }


        [SerializeField]
        private bool _shouldBounce;
        public bool ShouldBounce
        {
            get => _shouldBounce;
            set => _shouldBounce = value;
        }

        public event Action<RaycastHit> Impacted;

        [SerializeField]
        private float _pushPower;


        private void Update()
        {
            var direction = transform.forward;

            var distance = _speed * Time.deltaTime;

            var targetPosition = transform.position + direction * distance;


            if (Physics.Raycast(transform.position, direction, out var hit, distance))
            {
                if (hit.rigidbody)
                {
                    hit.rigidbody.AddForceAtPosition(-hit.normal * _speed * _pushPower, hit.point, ForceMode.Impulse);
                }

                if (ShouldDestroyOnColision)
                {
                    Destroy(gameObject);
                }

                if (ShouldDisableOnColision)
                {
                    enabled = false;
                }

                targetPosition = hit.point;

                Impacted?.Invoke(hit);

                if (ShouldBounce)
                {
                    var reflectedDirection = Vector3.Reflect(direction, hit.normal);
                    transform.forward = reflectedDirection; 
                }
            }

            Debug.DrawLine(transform.position, targetPosition, Color.red);

            transform.position = targetPosition;

            Debug.DrawRay(transform.position, direction * distance, Color.blue);

        }
    }
}