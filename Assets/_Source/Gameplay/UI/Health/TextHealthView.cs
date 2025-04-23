using TMPro;
using UnityEngine;

namespace _Source.Gameplay.UI.HealthView
{
    public class TextHealthView : HealthView
    {
        private const string HEALTH_FORMAT = "{0}/{1}";
        
        [SerializeField] private TextMeshProUGUI _healthText;

        protected override void OnHealthUpdated(float currentHealthNormalized, float maxHealth)
        {
            float currentHealth = Mathf.RoundToInt(currentHealthNormalized * maxHealth);
            _healthText.text = string.Format(HEALTH_FORMAT, currentHealth, maxHealth);
        }
    }
}