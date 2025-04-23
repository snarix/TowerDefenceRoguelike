using TMPro;
using UnityEngine;

namespace _Source.Gameplay.UI
{
    public class EnemyStatsTextView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentHealthText;
        
        public void UpdateHealthText(int health)
        {
            _currentHealthText.text = health.ToString();
        }
    }
}