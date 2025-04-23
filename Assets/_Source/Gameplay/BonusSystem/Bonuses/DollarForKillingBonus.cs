using _Source.Gameplay.BonusSystem.Abstractions;
using _Source.Gameplay.BonusSystem.Data;
using TowerDefenceRoguelike.Gameplay.Player;

namespace _Source.Gameplay.BonusSystem.Bonuses
{
    public class DollarForKillingBonus : PlayerStatsBonus
    {
        private float _value;

        public DollarForKillingBonus(PlayerStats playerStats, float value, BonusData bonusData) : base(playerStats, bonusData)
        {
            _value = value;
        }

        public override void Apply()
        {
            _playerStats.DollarForKillingMultiplier += _value;
        }
        public override BonusType BonusType => BonusType.DollarForKillingBonus;
        public override float GetApplingValue() => _value;
        public override float GetCurrentValue() => _playerStats.DollarForKillingMultiplier;
        public override float GetNextValue() => _playerStats.DollarForKillingMultiplier + _value;
    }
}