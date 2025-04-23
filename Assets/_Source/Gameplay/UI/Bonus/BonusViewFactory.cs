using _Source.Gameplay.BonusSystem.Abstractions;
using UnityEngine;

namespace _Source.Gameplay.UI.BonusView
{
    [CreateAssetMenu(menuName = "ScriptableObject/Factory/BonusView", fileName = "BonusView")]
    public class BonusViewFactory : ScriptableObject, IBonusViewFactory
    {
        private class BonusViewVisitor : IBonusVisitor
        {
            private PlayerStatsBonusView _playerStatsBonusView;
            private Transform _parentTransform;

            public BonusViewVisitor(PlayerStatsBonusView playerStatsBonusView, Transform parentTransform)
            {
                _playerStatsBonusView = playerStatsBonusView;
                _parentTransform = parentTransform;
            }

            public BonusView CurrentBonusView { get; private set; }

            public void Visit(PlayerStatsBonus playerStatsBonus)
            {
                var currentBonusView = Instantiate(_playerStatsBonusView, _parentTransform);
                
                currentBonusView.Initialize(playerStatsBonus.BonusData.BonusType, 
                    playerStatsBonus.BonusData.BonusName,
                    string.Format(playerStatsBonus.BonusData.BonusDescriptionFormat, playerStatsBonus.GetApplingValue()),
                    playerStatsBonus.GetCurrentValue(), 
                    playerStatsBonus.GetNextValue(),
                    playerStatsBonus.BonusData.BonusImage,
                    playerStatsBonus.BonusData.BonusDollarPrice,
                    playerStatsBonus.BonusData,
                    playerStatsBonus);
                
                CurrentBonusView = currentBonusView;
            }
        }

        [SerializeField] private PlayerStatsBonusView _playerStatsBonusView;
        
        public BonusView Create(IBonus bonus, Transform parentTransform)
        {
            var visitor = new BonusViewVisitor(_playerStatsBonusView, parentTransform);
            bonus.Accept(visitor);
            return visitor.CurrentBonusView;
        }
    }
}