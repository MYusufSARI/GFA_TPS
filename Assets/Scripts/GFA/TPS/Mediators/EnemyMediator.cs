using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFA.TPS.Mediators
{

    public class EnemyMediator : MonoBehaviour, IDamagable
    {
        [SerializeField]
        private float _health;

        private ItemDropper _itemDropper;

        private void Awake()
        {
            _itemDropper = GetComponent<ItemDropper>();
        }

        public void ApplyDamage(float damage, GameObject causer = null)
        {
            _health -= damage;

            if (_health <= 0)
            {
                gameObject.SetActive(false);

                if (_itemDropper)
                {
                    _itemDropper.OnDied();
                }
            }
            
        }

    }
}