using _Source.Gameplay.UI.BonusView;

namespace _Source.Gameplay.BonusSystem.Abstractions
{
    public interface IBonus
    {
        BonusType BonusType { get; }
        bool CanBeApplied();
        void Apply();
        void Accept(IBonusVisitor visitor);
    }
}