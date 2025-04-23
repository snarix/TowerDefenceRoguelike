using System.Collections.Generic;
using System.Linq;
using _Source.Gameplay.BonusSystem;
using _Source.Gameplay.BonusSystem.Abstractions;
using _Source.Gameplay.BonusSystem.Bonuses;
using _Source.Gameplay.BonusSystem.Data;
using TowerDefenceRoguelike.Gameplay.Player;

namespace _Source.MetaGameplay.MetaUpgrade
{
    public abstract class BonusApplierBase
    {
        protected List<IBonus> _bonuses;
        protected PlayerStats _playerStats;
        
        protected BonusApplierBase(BonusConfigHolder bonusConfig, PlayerStats playerStats)
        {
            _playerStats = playerStats;
            _bonuses = new List<IBonus>()
            {
                new DamageBonus(playerStats, bonusConfig.GetBonusData<IntBonusData>(BonusType.DamageBonus).BonusValue, bonusConfig.GetBonusData<IntBonusData>(BonusType.DamageBonus)),
                new RadiusBonus(playerStats, bonusConfig.GetBonusData<FloatBonusData>(BonusType.RadiusBonus).BonusValue, bonusConfig.GetBonusData<FloatBonusData>(BonusType.RadiusBonus)),
                new CooldownBonus(playerStats, bonusConfig.GetBonusData<FloatBonusData>(BonusType.CooldownBonus).BonusValue, bonusConfig.GetBonusData<FloatBonusData>(BonusType.CooldownBonus)),
                new MaxHealthBonus(playerStats, bonusConfig.GetBonusData<IntBonusData>(BonusType.MaxHealthBonus).BonusValue, bonusConfig.GetBonusData<IntBonusData>(BonusType.MaxHealthBonus)),
                new HealthRegenerationBonus(playerStats, bonusConfig.GetBonusData<FloatBonusData>(BonusType.HealthRegenerationBonus).BonusValue, bonusConfig.GetBonusData<FloatBonusData>(BonusType.HealthRegenerationBonus)),
                new GoldForKillingBonus(playerStats, bonusConfig.GetBonusData<FloatBonusData>(BonusType.GoldForKillingBonus).BonusValue, bonusConfig.GetBonusData<FloatBonusData>(BonusType.GoldForKillingBonus)),
                new GoldForWaveBonus(playerStats, bonusConfig.GetBonusData<FloatBonusData>(BonusType.GoldForWaveBonus).BonusValue, bonusConfig.GetBonusData<FloatBonusData>(BonusType.GoldForWaveBonus)),
                new DollarForKillingBonus(playerStats, bonusConfig.GetBonusData<FloatBonusData>(BonusType.DollarForKillingBonus).BonusValue, bonusConfig.GetBonusData<FloatBonusData>(BonusType.DollarForKillingBonus)),
                new DollarForWaveBonus(playerStats, bonusConfig.GetBonusData<FloatBonusData>(BonusType.DollarForWaveBonus).BonusValue, bonusConfig.GetBonusData<FloatBonusData>(BonusType.DollarForWaveBonus)),
            };
        }
    }
}