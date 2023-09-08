using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFA.TPS.BoosterSystem
{
    public abstract class Booster : MonoBehaviour
    {
        [SerializeField]
        private string _boosterName;
        public string BoosterName => _boosterName;

        [SerializeField]
        private string _description;
        public string Description => _description;


        public abstract void OnAdded(BoosterContainer container);
    }
}