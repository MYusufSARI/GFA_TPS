using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFA.TPS.WeaponSystem
{
    public class WeaponParticleFX : WeaponFX
    {
        [SerializeField]
        private ParticleSystem[] _particleSystems;

        public override void OnShoot()
        {
            foreach (var p in _particleSystems)
            {
                p.Play();
            }
        }
    }
}