using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFA.TPS
{

    public class Shooter : MonoBehaviour
    {
        [SerializeField]
        private float _fireRate = 0.5f;

        [SerializeField]
        private float _lastShootTime;

        public bool CanShoot { get; }

        public void Shoot()
        {
            if (!CanShoot)
            {
                return;
            }
        }
    }
}