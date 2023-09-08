using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFA.TPS
{
    [System.Serializable]
    public class Attributes
    {
        [SerializeField]
        private float damage = 0;
        public float Damage
        {
            get => damage;
            set => damage = value;
        }
        [SerializeField]

        private float movementSpeed = 5;
        public float MovementSpeed
        {
            get => movementSpeed;
            set => movementSpeed = value;
        }
        [SerializeField]

        private float attackSpeed = 1;
        public float AttackSpeed
        {
            get => attackSpeed;
            set => attackSpeed = value;
        }
        [SerializeField]

        private float defence = 0;
        public float Defence
        {
            get => defence;
            set => defence = Mathf.Clamp(value, 0, 0.95f);
        }
    }
}