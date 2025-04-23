using _Source.Gameplay.BonusSystem.Abstractions;
using _Source.Gameplay.UI.BonusView;
using _Source.MetaGameplay.MetaUpgrade.UpgradeView;
using UnityEngine;

namespace _Source.MetaGameplay.MetaUpgrade.Visitor
{
    [CreateAssetMenu(menuName = "ScriptableObject/Factory/UpgradeView", fileName = "UpgradeView")]
    public class UpgradeViewFactory : ScriptableObject, IBonusViewFactory
    {
        private class UpgradeVisitor : IBonusVisitor
        {
            private PlayerStatsUpgrade _upgrade;
            private Transform _parentTransform;

            public UpgradeVisitor(PlayerStatsUpgrade upgrade, Transform parentTransform)
            {
                _upgrade = upgrade;
                _parentTransform = parentTransform;
            }
            
            public BonusView CurrentUpgrade;
            
            public void Visit(PlayerStatsBonus bonus)
            {
                var upgrade = Instantiate(_upgrade, _parentTransform);
                upgrade.Initialize(bonus.BonusData.BonusType, 
                    bonus.BonusData.BonusAlternativeName,
                    bonus.GetCurrentValue(), 
                    bonus.GetNextValue(),
                    bonus.BonusData.BonusImage,
                    bonus.BonusData.BonusGoldPrice,
                    bonus.BonusData,
                    bonus);
                
                CurrentUpgrade = upgrade;
            }
        }
        
        [SerializeField] private PlayerStatsUpgrade _upgrade;

        public BonusView Create(IBonus bonus, Transform parentTransform)
        {
            var visitor = new UpgradeVisitor(_upgrade, parentTransform);
            bonus.Accept(visitor);
            return visitor.CurrentUpgrade;
        }
    }
}