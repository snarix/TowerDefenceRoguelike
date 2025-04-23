using System;
using System.Collections.Generic;
using _Source.Gameplay.AbilitySystem.Abstractions;
using _Source.Gameplay.UI.BonusView;
using UnityEngine;

namespace _Source.Gameplay.UI.Ability
{
    public class AbilityView : MonoBehaviour
    {
        [SerializeField] private GameObject _backgroundPanel;

        List<AbilityButton> _abilityButtons = new List<AbilityButton>();

        private IAbilityViewFactory _abilityViewFactory;

        public event Action<AbilityType> AbilityApply;

        public void Initialize(IAbilityViewFactory abilityViewFactory)
        {
            _abilityViewFactory = abilityViewFactory;
        }

        private void OnDestroy()
        {
            foreach (var abilityButton in _abilityButtons)
            {
                Destroy(abilityButton);
                abilityButton.AbilityApply -= OnAbilityApply;
            }
            _abilityButtons.Clear();
        }

        public void CreateAbility(List<IAbility> abilities)
        {
            foreach (var ability in abilities)
            {
                var abilityButton = _abilityViewFactory.Create(ability, _backgroundPanel.transform);
                _abilityButtons.Add(abilityButton);
                abilityButton.AbilityApply += OnAbilityApply;
            }
        }
        
        public void ShowPanelAbility() => _backgroundPanel.SetActive(true);
        public void HidePanelAbility() => _backgroundPanel.SetActive(false);

        private void OnAbilityApply(AbilityType abilityType) => AbilityApply?.Invoke(abilityType);
    }
}