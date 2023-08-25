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

        private void Update()
        {
            var direction = transform.forward;

            var distance = _speed * Time.deltaTime;

            var targetPosition = transform.position + direction * distance;

            if (Physics.Raycast(transform.position, direction, out var hit, distance))
            {
                targetPosition = hit.point;

            }

            Debug.DrawLine(transform.position, targetPosition, Color.red);

            transform.position = targetPosition;

            Debug.DrawRay(transform.position, direction * distance, Color.blue);

        }
    }
}