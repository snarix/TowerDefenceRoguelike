using System;
using System.Collections;
using System.Collections.Generic;
using _Source.Gameplay.UI;
using Include;
using TowerDefenceRoguelike.Gameplay.Base;
using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private Shooter _shooter;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private ParticleSystem _explosionFX;
        [SerializeField] private MeshRenderer _carVisual;
        [SerializeField] private float _explosionRadiusMultiplier = 7f;
        [SerializeField] private FadeAnimation _fadeAnimation;

        private PlayerStats _playerStats;
        private EnemyFinder.EnemyFinder _enemyFinder;
        private HealthRegeneration _healthRegeneration;
        private List<IDisposable> _disposables = new List<IDisposable>();

        public Health Health => _health;

        public HealthRegeneration HealthRegeneration => _healthRegeneration;

        public Shooter Shooter => _shooter;

        public event Action Died;
        public event Action<float> HealthRegenerationValueChanged; 

        public void Initialize(PlayerStats playerStats)
        {
            _playerStats = playerStats;

            _enemyFinder = new EnemyFinder.EnemyFinder(_enemyLayer);
            _shooter.Initialize(_enemyFinder, playerStats);

            var coroutineHandler = new MonoBehaviourCoroutineHandler(this);
            _healthRegeneration = new HealthRegeneration(_health, coroutineHandler);

            _health.SetMaxHealth(_playerStats.MaxHealth.Value);
            //_healthRegeneration.Activate(_playerStats.HealthRegeneration.Value);

            _disposables.Add(_healthRegeneration);

            _playerStats.MaxHealth.OnValueChanged += OnMaxHealthValueChanged;
            _playerStats.HealthRegeneration.OnValueChanged += OnHealthRegenerationValueChanged;
            _health.Died += OnHealthDied;
            _health.Damaged += OnDamaged;
        }

        private void OnDestroy()
        {
            _playerStats.MaxHealth.OnValueChanged -= OnMaxHealthValueChanged;
            _playerStats.HealthRegeneration.OnValueChanged -= OnHealthRegenerationValueChanged;
            _health.Died -= OnHealthDied;
            _health.Damaged -= OnDamaged;

            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }

        private void OnMaxHealthValueChanged(int maxHealth)
        {
            _health.SetMaxHealth(maxHealth);
        }

        private void OnHealthRegenerationValueChanged(float healthRegeneration) //?
        {
            _healthRegeneration.Activate(_playerStats.HealthRegeneration.Value);
            HealthRegenerationValueChanged?.Invoke(healthRegeneration);
            //_health.Heal(healthRegeneration);
        }
        
        private void OnDamaged(int damage)
        {
            _fadeAnimation.CreateVignette();
        }
        
        private void OnHealthDied(Health health)
        {
            var boomFx = Instantiate(_explosionFX, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
            boomFx.Play();
            
            EnemiesTakeDamageInRadiusExplosion();
            
            _carVisual.gameObject.SetActive(false);
            _shooter.gameObject.SetActive(false);
            
            StartCoroutine(WaitForExplosion());
            
            health.Died -= OnHealthDied;
        }

        private void EnemiesTakeDamageInRadiusExplosion()
        {
            ParticleSystem.ShapeModule shapeModule = _explosionFX.shape;
            float radius = shapeModule.radius * _explosionRadiusMultiplier;
            var nearestEnemies = _enemyFinder.FindAllEnemies(transform.position, radius);
            foreach (var enemy in nearestEnemies)
            {
                enemy.TakeDamage(_playerStats.Damage.Value * 3);
            }
        }

        private IEnumerator WaitForExplosion()
        {
            yield return new WaitForSeconds(_explosionFX.main.duration);

            gameObject.SetActive(false);
            Died?.Invoke();
        }
    }
}