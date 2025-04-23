using _Source.Gameplay.BonusSystem.Abstractions;
using _Source.Gameplay.BonusSystem.Data;
using TowerDefenceRoguelike.Gameplay.Player;

namespace _Source.Gameplay.BonusSystem.Bonuses
{
    public class CooldownBonus : PlayerStatsBonus
    {
        private float _value;
        
        public CooldownBonus(PlayerStats playerStats, float value, BonusData bonusData) : base(playerStats, bonusData)
        {
            _value = value;
        }

        public override void Apply()
        {
            _playerStats.Cooldown += _value;
        }
        public override BonusType BonusType => BonusType.CooldownBonus;
        public override float GetApplingValue() => _value;
        public override float GetCurrentValue() => _playerStats.Cooldown;
        public override float GetNextValue() => _playerStats.Cooldown + _value;
    }
}