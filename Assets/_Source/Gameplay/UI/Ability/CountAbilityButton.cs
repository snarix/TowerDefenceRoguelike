using _Source.Gameplay.AbilitySystem.Abstractions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.Gameplay.UI.Ability
{
    public class CountAbilityButton : AbilityButton
    { 
        [SerializeField] private Image _abilityIcon;
        [SerializeField] private TextMeshProUGUI _countText;
        private RestrictUsageCountAbility _countAbility;

        public void Initialize(AbilityType abilityType, RestrictUsageCountAbility countAbility, Image abilityIcon)
        {
            _abilityType = abilityType;
            _countAbility = countAbility;
            _abilityIcon.sprite = abilityIcon.sprite;

            _countText.text = _countAbility.GetCurrentUsageCount().ToString();
        }

        private void Update()
        {
            if (_countAbility != null)
            {
                var count = _countAbility.GetCurrentUsageCount();
                _countText.text = count.ToString();
            }
        }
    }
}