using _Source.Gameplay.BonusSystem.Abstractions;
using _Source.Gameplay.BonusSystem.Data;
using TowerDefenceRoguelike.Gameplay.Player;

namespace _Source.Gameplay.BonusSystem.Bonuses
{
    public class MaxHealthBonus : PlayerStatsBonus
    {
        private int _value;
        
        public MaxHealthBonus(PlayerStats playerStats, int value, BonusData bonusData) : base(playerStats, bonusData)
        {
            _value = value;
        }

        public override void Apply()
        {
            _playerStats.MaxHealth.Value += _value;
        }
        public override BonusType BonusType => BonusType.MaxHealthBonus;
        public override float GetApplingValue() => _value;
        public override float GetCurrentValue() => _playerStats.MaxHealth.Value;
        public override float GetNextValue() => _playerStats.MaxHealth.Value + _value;
    }
}