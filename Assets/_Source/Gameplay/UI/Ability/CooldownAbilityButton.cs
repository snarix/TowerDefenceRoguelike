using _Source.Gameplay.AbilitySystem.Abstractions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.Gameplay.UI.Ability
{
    public class CooldownAbilityButton : AbilityButton
    {
        [SerializeField] private Image _abilityIcon;
        [SerializeField] private TextMeshProUGUI _cooldownText;
        private CooldownAbility _cooldownAbility;
        
        public void Initialize(AbilityType abilityType, CooldownAbility cooldownAbility, Image abilityIcon)
        {
            _abilityType = abilityType;
            _cooldownAbility = cooldownAbility;
            _abilityIcon.sprite = abilityIcon.sprite;
            
            _cooldownText.text = _cooldownAbility.GetCooldown().ToString();
        }
        
        private void Update()
        {
            if (_cooldownAbility != null && !_cooldownAbility.CanBeUsed())
            {
                float remaining = _cooldownAbility.GetRemainingTime();
                _cooldownText.text = Mathf.RoundToInt(remaining).ToString();
            }

            if (_cooldownAbility != null && _cooldownAbility.CanBeUsed())
            {
                _cooldownText.text = _cooldownAbility.GetCooldown().ToString();
            } 
        }
    }
}