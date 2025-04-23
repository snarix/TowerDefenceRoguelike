using TMPro;
using TowerDefenceRoguelike.Gameplay.Base;
using UnityEngine;

namespace _Source.Gameplay.UI.HealthView
{
    public class TextDamageView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _damageText;
        [SerializeField] private KillTextAnimation _killTextAnimation;
        private Health _health;
        
        public void Initialize(Health health)
        {
            _health = health;
            _health.Damaged += OnHealthDamaged;
        }
        
        private void OnHealthDamaged(int damage)
        {
            //enabled = false;
            _killTextAnimation.Show();
            gameObject.SetActive(true);
            _damageText.text = damage.ToString();
        }
    }
}