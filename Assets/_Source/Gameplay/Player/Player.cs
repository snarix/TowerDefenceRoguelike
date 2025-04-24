using System;
using System.Collections.Generic;
using Include;
using TowerDefenceRoguelike.Gameplay.Base;
using TowerDefenceRoguelike.Gameplay.Player.Abstractions;
using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private Shooter _shooter;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private PlayerExplosionFxView _playerExplosionFxView;

        private PlayerStats _playerStats;
        private IEnemyFinder _enemyFinder;
        private HealthRegeneration _healthRegeneration;
        private MultiplierConfig _multiplierConfig;
        private List<IDisposable> _disposables = new List<IDisposable>();

        public Health Health => _health;

        public Shooter Shooter => _shooter;

        public event Action Died;
        public event Action<float> HealthRegenerationValueChanged; 
        public event Action<int> DamageValueChanged; 

        public void Initialize(PlayerStats playerStats, MultiplierConfig config)
        {
            _playerStats = playerStats;
            _multiplierConfig = config;
            
            _enemyFinder = new EnemyFinder.EnemyFinder(_enemyLayer);
            _shooter.Initialize(_enemyFinder, playerStats);
            _playerExplosionFxView.Initialize(this);

            var coroutineHandler = new MonoBehaviourCoroutineHandler(this);
            _healthRegeneration = new HealthRegeneration(_health, coroutineHandler);

            _health.SetMaxHealth(_playerStats.MaxHealth.Value);

            _disposables.Add(_healthRegeneration);
            
            _playerStats.MaxHealth.OnValueChanged += OnMaxHealthValueChanged;
            _playerStats.HealthRegeneration.OnValueChanged += OnHealthRegenerationValueChanged;
            _playerStats.Damage.OnValueChanged += DamageOnValueChanged;
            _health.Died += OnHealthDied;
        }

        private void OnDestroy()
        {
            _playerStats.MaxHealth.OnValueChanged -= OnMaxHealthValueChanged;
            _playerStats.HealthRegeneration.OnValueChanged -= OnHealthRegenerationValueChanged;
            _playerStats.Damage.OnValueChanged -= DamageOnValueChanged;
            _health.Died -= OnHealthDied;

            foreach (var disposable in _disposables)
                disposable.Dispose();
        }

        private void OnMaxHealthValueChanged(int maxHealth)
        {
            _health.SetMaxHealth(maxHealth);
        }

        private void OnHealthRegenerationValueChanged(float healthRegeneration)
        {
            _healthRegeneration.Activate(_playerStats.HealthRegeneration.Value);
            HealthRegenerationValueChanged?.Invoke(healthRegeneration);
        }
        
        private void DamageOnValueChanged(int damage)
        {
            DamageValueChanged?.Invoke(damage);
        }
        
        private void OnHealthDied(Health health)
        {
            EnemiesTakeDamageInRadiusExplosion();
            Died?.Invoke();
            health.Died -= OnHealthDied;
        }

        private void EnemiesTakeDamageInRadiusExplosion()
        {
            float radius = _playerExplosionFxView.GetExplosionRadius();
            var nearestEnemies = _enemyFinder.FindAllEnemies(transform.position, radius);
            
            foreach (var enemy in nearestEnemies)
                enemy.TakeDamage(_playerStats.Damage.Value * _multiplierConfig.Multiplier);
        }
    }
}