using _Source.Gameplay.UI.BonusView;

namespace _Source.Gameplay.AbilitySystem.Abstractions
{
    public interface IAbility
    {
        AbilityType Type { get; }
        bool CanBeUsed();
        void Use();
        void Accept(IAbilityVisitor visitor);
    }
}