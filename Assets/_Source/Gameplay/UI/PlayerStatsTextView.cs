using TMPro;
using UnityEngine;

namespace _Source.Gameplay.UI
{
    public class PlayerStatsTextView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentDamageText;
        [SerializeField] private TextMeshProUGUI _currentHealthRegenerationText;

        public void UpdateDamageText(int damage)
        {
            _currentDamageText.text = damage.ToString();
        }
        
        public void UpdateHealthRegenerationText(float healthRegeneration)
        {
            _currentHealthRegenerationText.text = $"{healthRegeneration}/C";
        }
    }
}