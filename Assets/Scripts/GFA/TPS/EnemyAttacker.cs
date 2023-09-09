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

        public float Range => _range;

        [SerializeField]
        private float _attackRate;

        private float _lastAttack;

        public bool CanAttack =>Time.time > _lastAttack + _attackRate;

        public bool IsCurrentlyAttacking { get; private set; }

        public event Action<IDamagable> Attacked;

        private IDamagable _currentTarget;



        public void Attack(IDamagable target)
        {
            if (!CanAttack == false)
            {
                return;
            }
            _lastAttack = Time.time;
            Attacked?.Invoke(target);
            StartCoroutine(ApplyAttackDelayed(target));
        }


        public void ExecuteDamage()
        {

        }


        private IEnumerator ApplyAttackDelayed(IDamagable target)
        {
            IsCurrentlyAttacking = true;
            yield return new WaitForSeconds(0.5f);
            IsCurrentlyAttacking = false;

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