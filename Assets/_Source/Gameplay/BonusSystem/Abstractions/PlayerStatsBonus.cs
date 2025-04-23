using _Source.Gameplay.BonusSystem.Data;
using _Source.Gameplay.UI.BonusView;
using TowerDefenceRoguelike.Gameplay.Player;

namespace _Source.Gameplay.BonusSystem.Abstractions
{
    public abstract class PlayerStatsBonus : IBonus
    {
        protected PlayerStats _playerStats;
        
        protected PlayerStatsBonus(PlayerStats playerStats, BonusData bonusData)
        {
            _playerStats = playerStats;
            BonusData = bonusData;
        }

        public BonusData BonusData { get; }
        public abstract BonusType BonusType { get; }
        
        public void Accept(IBonusVisitor visitor)
        {
            visitor.Visit(this);
        }
        
        public virtual bool CanBeApplied() => true;

        public abstract void Apply();

        public abstract float GetApplingValue();
        public abstract float GetCurrentValue();
        public abstract float GetNextValue();
    }
}