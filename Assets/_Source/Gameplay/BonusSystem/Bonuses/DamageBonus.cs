using _Source.Gameplay.BonusSystem.Abstractions;
using _Source.Gameplay.BonusSystem.Data;
using TowerDefenceRoguelike.Gameplay.Player;

namespace _Source.Gameplay.BonusSystem.Bonuses
{
    public class DamageBonus : PlayerStatsBonus
    {
        private int _value;
        
        public DamageBonus(PlayerStats playerStats, int value, BonusData bonusData) : base(playerStats, bonusData)
        {
            _value = value;
        }

        public override BonusType BonusType => BonusType.DamageBonus;

        public override void Apply()
        {
            _playerStats.Damage.Value += _value;
        }
        
        public override float GetApplingValue() => _value;
        public override float GetCurrentValue() => _playerStats.Damage.Value;
        public override float GetNextValue() => _playerStats.Damage.Value + _value;
    }
}