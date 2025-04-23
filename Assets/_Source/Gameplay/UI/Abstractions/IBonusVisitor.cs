using _Source.Gameplay.BonusSystem.Abstractions;

namespace _Source.Gameplay.UI.BonusView
{
    public interface IBonusVisitor
    {
        void Visit(PlayerStatsBonus playerStatsBonus);
    }
}