using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using UnityEngine;

namespace GFA.TPS
{
    public class EnemyAttacker : MonoBehaviour
    {
        [SerializeField]
        private float _damage;

        [SerializeField]
        private float _range;

        [SerializeField]
        private float _attackRate;

        private float _lastAttack;

        public bool CanAttack => _lastAttack + _attackRate > Time.time;

        public void Attack(IDamagable target)
        {
            if (!CanAttack == false)
            {
                return;
            }
            _lastAttack = Time.time;
            StartCoroutine(ApplyAttackDelayed(target));
        }


        private IEnumerator ApplyAttackDelayed(IDamagable target)
        {
            yield return new WaitForSeconds(0.5f);
            if (target is MonoBehaviour mb)
            {
                if (Vector3.Distance(mb.transform.position, transform.position) < _range)
                {
                    target.ApplyDamage(_damage);
                }
            }
            else
            {
                target.ApplyDamage(_damage);
            }
        }
    }
}