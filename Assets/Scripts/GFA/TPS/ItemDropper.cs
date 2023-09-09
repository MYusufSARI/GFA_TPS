using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GFA.TPS.WeaponSystem;
using UnityEngine;

namespace GFA.TPS
{
    public class ItemDropper : MonoBehaviour
    {
        [SerializeField]
        private float _xp;

        [SerializeField, Range(0, 1)]
        private float _xpDropChange;

        [SerializeField]
        private XPCollectible _xpCollectiblePrefab;

        [SerializeField]
        private WeaponDropChance[] _weaponDropChances;

        [SerializeField]
        private WeaponCollectible _weaponColletiblePrefab;


        public void OnDied()
        {
            if (_xpCollectiblePrefab && Random.value < _xpDropChange)
            {
                var inst = Instantiate(_xpCollectiblePrefab, transform.position, Quaternion.identity);

                inst.XP = _xp;
            }

            foreach (var weaponDrop in _weaponDropChances)
            {
                if (Random.value < weaponDrop.Chance)
                {
                    var inst = Instantiate(_weaponColletiblePrefab, transform.position, Quaternion.identity);
                    inst.Weapon = weaponDrop.Weapon;
                    Vector3 randomPointOnCircle = Random.insideUnitCircle;

                    randomPointOnCircle.z = randomPointOnCircle.y;
                    randomPointOnCircle.y = 0;

                    inst.transform.DOJump((transform.position + randomPointOnCircle) * 5, 1, 1, 0.4f);
                    break;
                }
            }
        }
        [System.Serializable]

        public class WeaponDropChance
        {
            public float Chance;
            public Weapon Weapon;
        }
    }
}