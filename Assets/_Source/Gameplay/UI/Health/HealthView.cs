using TowerDefenceRoguelike.Gameplay.Base;
using UnityEngine;

namespace _Source.Gameplay.UI.HealthView
{
    public abstract class HealthView : MonoBehaviour
    {
        private Health _health;

        public void Initialize(Health health)
        {
            _health = health;
            _health.Updated += OnHealthUpdated;

            OnHealthUpdated(_health.CurrentHealthNormalized, _health.MaxHealth);
        }

        private void OnDestroy()
        {
            if (_health != null)
                _health.Updated -= OnHealthUpdated;
        }

        private void OnHealthUpdated(float health)
        {
            OnHealthUpdated(health, _health.MaxHealth);
        }

        protected abstract void OnHealthUpdated(float currentHealthNormalized, float maxHealth);
    }
}