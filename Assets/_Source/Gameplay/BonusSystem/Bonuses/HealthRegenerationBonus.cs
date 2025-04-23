using _Source.Gameplay.BonusSystem.Abstractions;
using _Source.Gameplay.BonusSystem.Data;
using TowerDefenceRoguelike.Gameplay.Player;

namespace _Source.Gameplay.BonusSystem.Bonuses
{
    public class HealthRegenerationBonus : PlayerStatsBonus
    {
        private float _value;

        public HealthRegenerationBonus(PlayerStats playerStats, float value, BonusData bonusData) : base(playerStats, bonusData)
        {
            _value = value;
        }

        public override void Apply()
        {
            _playerStats.HealthRegeneration.Value += _value;
        }

        public override BonusType BonusType => BonusType.HealthRegenerationBonus;
        
        public override float GetApplingValue() => _value;
        public override float GetCurrentValue() => _playerStats.HealthRegeneration.Value;
        public override float GetNextValue() => _playerStats.HealthRegeneration.Value + _value;
    }
}