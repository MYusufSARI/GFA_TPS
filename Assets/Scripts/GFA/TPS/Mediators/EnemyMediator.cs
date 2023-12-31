using System;
using System.Collections;
using System.Collections.Generic;
using GFA.TPS.AI;
using GFA.TPS.Animating;
using UnityEngine;

namespace GFA.TPS.Mediators
{

    public class EnemyMediator : MonoBehaviour, IDamagable
    {
        [SerializeField]
        private float _health;

        private ItemDropper _itemDropper;

        private EnemyAttacker _attacker;

        private EnemyAnimation _enemyAnimation;

        private AIController _aiController;

        private void Awake()
        {
            _itemDropper = GetComponent<ItemDropper>();
            _attacker = GetComponent<EnemyAttacker>();
            _enemyAnimation = GetComponent<EnemyAnimation>();
            _aiController = GetComponent<AIController>();

        }

        private void OnEnable()
        {
            _attacker.Attacked += OnAttackerAttacked;
        }

        private void OnDisable()
        {
            _attacker.Attacked -= OnAttackerAttacked;
        }

        private void OnAttackerAttacked(IDamagable damagable)
        {
            _enemyAnimation.PlayAttackAnimation();
        }

        public void ApplyDamage(float damage, GameObject causer = null)
        {
            _health -= damage;

            if (_health <= 0)
            {
                _enemyAnimation.PlayDeathAnimation();

                _aiController.enabled = false;

                StartCoroutine(DisableDelayed());

                if (_itemDropper)
                {
                    _itemDropper.OnDied();
                }
            }

        }

        private IEnumerator DisableDelayed()
        {
            yield return new WaitForSeconds(2);

            gameObject.SetActive(false);

        }
    }
}