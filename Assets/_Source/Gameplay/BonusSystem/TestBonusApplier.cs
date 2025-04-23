using System;
using System.Collections.Generic;
using System.Linq;
using _Source.Gameplay.BonusSystem.Abstractions;
using _Source.Gameplay.BonusSystem.Data;
using _Source.Gameplay.Currency;
using _Source.Gameplay.UI;
using _Source.Gameplay.UI.BonusView;
using _Source.MetaGameplay.MetaUpgrade;
using _Source.MetaGameplay.Transition;
using Include;
using TowerDefenceRoguelike.Gameplay.Player;
using Random = UnityEngine.Random;

namespace _Source.Gameplay.BonusSystem
{
    public class TestBonusApplier : BonusApplierBase, IDisposable
    {
        private BonusPanelView _bonusApplierView;
        
        public TestBonusApplier(BonusConfigHolder bonusConfig, PlayerStats playerStats, BonusPanelView bonusApplierView) : base(bonusConfig, playerStats)
        {
            _bonusApplierView = bonusApplierView;
            _bonusApplierView.BonusApplied += OnBonusApplied;
            _bonusApplierView.ReRolled += OnReRolled;
            _bonusApplierView.Confirmed += OnConfirmed;
            
            foreach (var bonus in _bonuses)
            {
                if (bonus is PlayerStatsBonus playerStatsBonus)
                {
                    playerStatsBonus.BonusData.ResetToGameplayPrice();
                }
            }
        }
        
        public event Action Confirmed;

        public void Dispose() => _bonusApplierView.BonusApplied -= OnBonusApplied;

        public void ShowBonuses()
        {
            var randomBonuses = GetRandomBonus();
            _bonusApplierView.ShowBonuses(randomBonuses);
        }
        
        private void OnReRolled(ShakePositionAnimation shakePositionAnimation)
        {
            var wallet = ServiceLocator.GetService<GameplayWallet>();
            if (wallet.Dollar.IsEnough(_bonusApplierView.ChangePrice))
            {
                shakePositionAnimation.Unlock();
                wallet.Dollar.Spend(_bonusApplierView.ChangePrice);
                _bonusApplierView.IncreasePriceText();
                DestroyBonuses();
                ShowBonuses();
            }
            else
            {
                shakePositionAnimation.OnButtonShakeClick();
                shakePositionAnimation.Lock();
            }
        }
        
        private void OnBonusApplied(BonusType bonusType)
        {
            var bonus = _bonuses.FirstOrDefault(b => b.BonusType == bonusType);
            if (bonus is PlayerStatsBonus playerStatsBonus)
            {
                var data = ServiceLocator.GetService<GameplayWallet>();
                data.Dollar.Spend(playerStatsBonus.BonusData.BonusDollarPrice);
                
                playerStatsBonus.BonusData.IncreaseDollarPrice();
                
                bonus.Apply();
                _bonusApplierView.MakeBonusVoid(bonusType);
            }
        }
        
        public void DestroyBonuses() => _bonusApplierView.DestroyBonuses();
        
        public void ShowConfirmReRollButton() => _bonusApplierView.ShowConfirmReRollButton();

        public void HideConfirmReRollButton() => _bonusApplierView.HideConfirmReRollButton();
        
        private List<IBonus> GetRandomBonus() => _bonuses.OrderBy(x => Random.Range(0, _bonuses.Count)).Take(3).ToList();
        
        private void OnConfirmed() => Confirmed?.Invoke();
    }
}