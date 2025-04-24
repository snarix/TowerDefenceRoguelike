using _Source.Gameplay.AbilitySystem.Abstractions;
using _Source.Gameplay.UI.BonusView;
using UnityEngine;

namespace _Source.Gameplay.UI.Ability
{
    [CreateAssetMenu(menuName = "ScriptableObject/Factory/AbilityButton", fileName = "AbilityButton")]
    public class AbilityButtonFactory : ScriptableObject, IAbilityViewFactory
    {
        private class AbilityButtonVisitor : IAbilityVisitor
        {
            private CooldownAbilityButton _cooldownAbilityButton;
            private CountAbilityButton _countAbilityButton;
            private Transform _parentTransform;

            public AbilityButton CurrentAbilityButton { get; private set; }

            public AbilityButtonVisitor(CooldownAbilityButton cooldownAbilityButton, CountAbilityButton countAbilityButton, Transform parentTransform)
            {
                _cooldownAbilityButton = cooldownAbilityButton;
                _countAbilityButton = countAbilityButton;
                _parentTransform = parentTransform;
            }

            public void Visit(CooldownAbility cooldownAbility)
            {
                var ability = Instantiate(_cooldownAbilityButton, _parentTransform);
                ability.Initialize(cooldownAbility.Type, cooldownAbility, cooldownAbility.Data.AbilityImage);
                CurrentAbilityButton = ability;
            }

            public void Visit(RestrictUsageCountAbility restrictUsageCountAbility)
            {
                var ability = Instantiate(_countAbilityButton, _parentTransform);
                ability.Initialize(restrictUsageCountAbility.Type, restrictUsageCountAbility, restrictUsageCountAbility.Data.AbilityImage);
                CurrentAbilityButton = ability;
            }
        }
        
        [SerializeField] private CooldownAbilityButton _abilityButton;
        [SerializeField] private CountAbilityButton _countAbilityButton;

        public AbilityButton Create(IAbility ability, Transform parent)
        {
            var visiter = new AbilityButtonVisitor(_abilityButton,_countAbilityButton, parent);
            ability.Accept(visiter);
            return visiter.CurrentAbilityButton;
        }
    }
}