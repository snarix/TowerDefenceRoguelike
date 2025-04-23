using System;
using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Player
{
    [Serializable]
    public class PlayerStats
    {
        [SerializeField] private ReactiveValue<int> _damage;
        [SerializeField] private float _cooldown;
        [SerializeField] private ReactiveValue<float>  _radius;
        
        [SerializeField] private ReactiveValue<int> _maxHealth;
        [SerializeField] private ReactiveValue<float> _healthRegeneration;

        [SerializeField] private float _dollarForWaveMultiplier;
        [SerializeField] private float _goldForWaveMultiplier;
        [SerializeField] private float _dollarForKillingMultiplier;
        [SerializeField] private float _goldForKillingMultiplier;

        public PlayerStats(int damage, float cooldown, float radius, int maxHealth, float healthRegeneration, float dollarForWaveMultiplier, float goldForWaveMultiplier, float dollarForKillingMultiplier, float goldForKillingMultiplier)
        {
            _damage = new ReactiveValue<int>(damage);
            _cooldown = cooldown;
            _radius = new ReactiveValue<float>(radius);
            _maxHealth = new ReactiveValue<int>(maxHealth);
            _healthRegeneration = new ReactiveValue<float>(healthRegeneration);
            _dollarForWaveMultiplier = dollarForWaveMultiplier;
            _goldForWaveMultiplier = goldForWaveMultiplier;
            _dollarForKillingMultiplier = dollarForKillingMultiplier;
            _goldForKillingMultiplier = goldForKillingMultiplier;
        }

        public ReactiveValue<int> MaxHealth => _maxHealth;

        public ReactiveValue<float> HealthRegeneration => _healthRegeneration;

        public ReactiveValue<float>  Radius => _radius;

        public ReactiveValue<int> Damage => _damage;

        public float Cooldown
        {
            get => _cooldown;
            set => _cooldown = value;
        }
        
        public float DollarForWaveMultiplier
        {
            get => _dollarForWaveMultiplier;
            set => _dollarForWaveMultiplier = value;
        }

        public float GoldForWaveMultiplier
        {
            get => _goldForWaveMultiplier;
            set => _goldForWaveMultiplier = value;
        }

        public float DollarForKillingMultiplier
        {
            get => _dollarForKillingMultiplier;
            set => _dollarForKillingMultiplier = value;
        }

        public float GoldForKillingMultiplier
        {
            get => _goldForKillingMultiplier;
            set => _goldForKillingMultiplier = value;
        }
    }
}