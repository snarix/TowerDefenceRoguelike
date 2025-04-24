using System;
using System.Linq;
using _Source.Gameplay.BonusSystem;
using _Source.Gameplay.BonusSystem.Abstractions;
using _Source.Gameplay.BonusSystem.Data;
using _Source.Gameplay.Currency;
using _Source.MetaGameplay.MetaUpgrade.UpgradeView;
using _Source.MetaGameplay.Transition;
using Include;
using TowerDefenceRoguelike.Gameplay.Player;

namespace _Source.MetaGameplay.MetaUpgrade
{
    public class MetaUpgradeApplier : BonusApplierBase, IDisposable
    {
        private MetaUpgradePanelView _panelView;

        public MetaUpgradeApplier(BonusConfigHolder bonusConfig, PlayerStats playerStats, MetaUpgradePanelView panelView) : base(bonusConfig, playerStats)
        {
            _panelView = panelView;
            _panelView.ShowUpgrades(_bonuses);
            _panelView.UpgradeApplied += OnBonusUpgraded;
        }

        public void Dispose() => _panelView.UpgradeApplied -= OnBonusUpgraded;
        
        private int _tempPrice;
        
        private void OnBonusUpgraded(BonusType bonusType)
        {
            var bonus = _bonuses.FirstOrDefault(b => b.BonusType == bonusType);
            if (bonus is PlayerStatsBonus playerStatsBonus)
            {
                var wallet = ServiceLocator.GetService<MetaWallet>();
                wallet.Gold.Spend(playerStatsBonus.BonusData.BonusGoldPrice);
                
                playerStatsBonus.BonusData.IncreaseGoldPrice();
                
                var bonusPriceDataLoader = ServiceLocator.GetService<BonusPriceDataLoader>();
                bonusPriceDataLoader.SaveBonusPrice();
                
                bonus.Apply();
                
                var walletDataLoader = ServiceLocator.GetService<WalletDataLoader>();
                walletDataLoader.SaveWallet(wallet);
                
                var playerStatsDataLoader = ServiceLocator.GetService<PlayerStatsDataLoader>();
                playerStatsDataLoader.SavePlayerStats(_playerStats);
            }
        }
    }
}