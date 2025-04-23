using System;
using TowerDefenceRoguelike.Gameplay.Base.Abstractions;
using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Base
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _maxHealth;

        private float _currentHealth;

        private bool _isDead = false;

        public event Action<int> Damaged;
        public event Action<float> Updated;
        public event Action<Health> Died;

        public Vector3 Position => transform.position;

        public float MaxHealth => _maxHealth;

        public float CurrentHealth => _currentHealth;

        public float CurrentHealthNormalized => _currentHealth / _maxHealth;

        private void Awake() => _currentHealth = _maxHealth;

        public void TakeDamage(int damage)
        {
            if (_isDead)
                return;
            if (damage <= 0)
                throw new ArgumentException($"{nameof(damage)} must be greater than 0");

            _currentHealth = Mathf.Max(_currentHealth - damage, 0);

            Damaged?.Invoke(damage);

            if (_currentHealth <= 0)
                Kill();
            
            Updated?.Invoke(CurrentHealthNormalized);
        }

        public void SetMaxHealth(int newMaxHealth)
        {
            if (_isDead)
                return;

            float healthPercent = newMaxHealth / _maxHealth;
            _maxHealth = newMaxHealth;
            _currentHealth = Mathf.RoundToInt(healthPercent * _currentHealth);

            Updated?.Invoke(CurrentHealthNormalized);
        }

        public void Heal(float amount)
        {
            if (_isDead)
                return;

            if (_currentHealth + amount <= _maxHealth)
                _currentHealth += amount;
            else
                _currentHealth = _maxHealth;

            Updated?.Invoke(CurrentHealthNormalized);
        }

        private void Kill()
        {
            _isDead = true;
            Died?.Invoke(this);
        }
    }
}