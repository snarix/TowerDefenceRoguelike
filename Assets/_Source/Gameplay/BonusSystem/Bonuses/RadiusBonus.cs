using _Source.Gameplay.BonusSystem.Abstractions;
using _Source.Gameplay.BonusSystem.Data;
using TowerDefenceRoguelike.Gameplay.Player;

namespace _Source.Gameplay.BonusSystem.Bonuses
{
    public class RadiusBonus : PlayerStatsBonus
    {
        private float _value;
        
        public RadiusBonus(PlayerStats playerStats, float value, BonusData bonusData) : base(playerStats, bonusData)
        {
            _value = value;
        }

        public override void Apply()
        {
            _playerStats.Radius.Value += _value;
        }
        public override BonusType BonusType => BonusType.RadiusBonus;
        public override float GetApplingValue() => _value;
        public override float GetCurrentValue() => _playerStats.Radius.Value;
        public override float GetNextValue() => _playerStats.Radius.Value + _value;
    }
}