using System;
using _Source.Gameplay.AbilitySystem.Abstractions;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.Gameplay.UI.Ability
{
    public abstract class AbilityButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        protected AbilityType _abilityType;
        public event Action<AbilityType> AbilityApply;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            AbilityApply?.Invoke(_abilityType);
        }
    }
}