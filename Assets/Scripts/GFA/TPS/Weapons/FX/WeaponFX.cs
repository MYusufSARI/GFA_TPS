using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFA.TPS.WeaponSystem
{
    public abstract class WeaponFX : MonoBehaviour
    {
        private WeaponGraphics _weaponGraphics;

        private void Awake()
        {
            _weaponGraphics = GetComponent<WeaponGraphics>();
        }

        private void OnEnable()
        {
            _weaponGraphics.Shoot += OnShoot;
        }

        private void OnDisable()
        {
            _weaponGraphics.Shoot -= OnShoot;

        }

        public abstract void OnShoot();
        
    }
}