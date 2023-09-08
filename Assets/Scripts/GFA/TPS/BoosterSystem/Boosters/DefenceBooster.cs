using System.Collections;
using System.Collections.Generic;
using GFA.TPS.Mediators;
using UnityEngine;

namespace GFA.TPS.BoosterSystem.Boosters
{
    [CreateAssetMenu(menuName = "Defence")]

    public class DefenceBooster : Booster
    {
        [SerializeField]
        private float _value;

        public override void OnAdded(BoosterContainer container)
        {
            if (container.TryGetComponent<PlayerMediator>(out var mediator))
            {
                mediator.Attributes.Defence += _value;
            }
        }
    }
}
