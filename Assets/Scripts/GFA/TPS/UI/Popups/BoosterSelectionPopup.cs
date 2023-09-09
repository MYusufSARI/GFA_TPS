using DG.Tweening;
using GFA.TPS.BoosterSystem;
using UnityEngine;

namespace GFA.TPS.UI.Popups
{
    public class BoosterSelectionPopup : Popup
    {
        [SerializeField]
        private BoosterList _boosterList;

        [SerializeField]
        private BoosterCard _boosterCardPrefab;

        [SerializeField]
        private Transform _container;

        

        protected override void OnOpened()
        {
            base.OnOpened();

            for (int i = 0; i < 4; i++)
            {
                var randomBooster = _boosterList.Get(Random.Range(0, _boosterList.Length));
                var inst = Instantiate(_boosterCardPrefab, _container);
                inst.Booster = randomBooster;
            }
        }
    }
}