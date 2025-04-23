using System;
using System.Collections.Generic;
using _Source.Gameplay.BonusSystem;
using _Source.Gameplay.BonusSystem.Abstractions;
using _Source.Gameplay.UI.BonusView;
using UnityEngine;

namespace _Source.MetaGameplay.MetaUpgrade.UpgradeView
{
    public class MetaUpgradePanelView : MonoBehaviour
    {
        [SerializeField] private Transform _upgradeContainer;

        private List<BonusView> _bonusViews = new List<BonusView>();
        private IBonusViewFactory _factory;

        public event Action<BonusType> UpgradeApplied;

        public void Initialize(IBonusViewFactory factory)
        {
            _factory = factory;
        }
        
        public void ShowUpgrades(IEnumerable<IBonus> upgrades)
        {
            foreach (var upgrade in upgrades)
            {
                var buttonUpgrade= _factory.Create(upgrade, _upgradeContainer);
                buttonUpgrade.BonusApplied += OnButtonClicked;
                _bonusViews.Add(buttonUpgrade);
            }
        }

        private void OnDestroy()
        {
            foreach (var bonusView in _bonusViews)
            {
                bonusView.BonusApplied -= OnButtonClicked;
            }
            _bonusViews.Clear();
        }

        private void OnButtonClicked(BonusType bonusType)
        {
            UpgradeApplied?.Invoke(bonusType);
        }
    }
}