using _Source.Gameplay.AbilitySystem.Abstractions;

namespace _Source.Gameplay.UI.BonusView
{
    public interface IAbilityVisitor
    {
        void Visit(CooldownAbility cooldownAbility);
        void Visit(RestrictUsageCountAbility restrictUsageCountAbility);
    }
}