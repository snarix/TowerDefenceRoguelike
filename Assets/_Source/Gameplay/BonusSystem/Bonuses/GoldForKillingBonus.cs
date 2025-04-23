using _Source.Gameplay.BonusSystem.Abstractions;
using _Source.Gameplay.BonusSystem.Data;
using TowerDefenceRoguelike.Gameplay.Player;

namespace _Source.Gameplay.BonusSystem.Bonuses
{
    public class GoldForKillingBonus : PlayerStatsBonus
    {
        private float _value;

        public GoldForKillingBonus(PlayerStats playerStats, float value, BonusData bonusData) : base(playerStats, bonusData)
        {
            _value = value;
        }

        public override void Apply()
        {
            _playerStats.GoldForKillingMultiplier += _value;
        }
        public override BonusType BonusType => BonusType.GoldForKillingBonus;
        public override float GetApplingValue() => _value;
        public override float GetCurrentValue() => _playerStats.GoldForKillingMultiplier;
        public override float GetNextValue() => _playerStats.GoldForKillingMultiplier + _value;
    }
}