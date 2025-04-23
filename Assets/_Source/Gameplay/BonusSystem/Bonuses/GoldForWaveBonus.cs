using _Source.Gameplay.BonusSystem.Abstractions;
using _Source.Gameplay.BonusSystem.Data;
using TowerDefenceRoguelike.Gameplay.Player;

namespace _Source.Gameplay.BonusSystem.Bonuses
{
    public class GoldForWaveBonus : PlayerStatsBonus
    {
        private float _value;

        public GoldForWaveBonus(PlayerStats playerStats, float value, BonusData bonusData) : base(playerStats, bonusData)
        {
            _value = value;
        }

        public override void Apply()
        {
            _playerStats.GoldForWaveMultiplier += _value;
        }
        public override BonusType BonusType => BonusType.GoldForWaveBonus;
        public override float GetApplingValue() => _value;
        public override float GetCurrentValue() => _playerStats.GoldForWaveMultiplier;
        public override float GetNextValue() => _playerStats.GoldForWaveMultiplier + _value;
    }
}