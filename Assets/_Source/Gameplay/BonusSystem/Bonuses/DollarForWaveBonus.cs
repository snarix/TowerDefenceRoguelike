using _Source.Gameplay.BonusSystem.Abstractions;
using _Source.Gameplay.BonusSystem.Data;
using TowerDefenceRoguelike.Gameplay.Player;

namespace _Source.Gameplay.BonusSystem.Bonuses
{
    public class DollarForWaveBonus : PlayerStatsBonus
    {
        private float _value;

        public DollarForWaveBonus(PlayerStats playerStats, float value, BonusData bonusData) : base(playerStats, bonusData)
        {
            _value = value;
        }

        public override void Apply()
        {
            _playerStats.DollarForWaveMultiplier += _value;
        }
        public override BonusType BonusType => BonusType.DollarForWaveBonus;
        public override float GetApplingValue() => _value;
        public override float GetCurrentValue() => _playerStats.DollarForWaveMultiplier;
        public override float GetNextValue() => _playerStats.DollarForWaveMultiplier + _value;
    }
}