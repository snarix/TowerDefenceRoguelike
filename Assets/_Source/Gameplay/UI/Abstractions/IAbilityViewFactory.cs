using _Source.Gameplay.AbilitySystem.Abstractions;
using _Source.Gameplay.UI.Ability;
using UnityEngine;

namespace _Source.Gameplay.UI.BonusView
{
    public interface IAbilityViewFactory
    {
        AbilityButton Create(IAbility ability, Transform parentTransform);
    }
}