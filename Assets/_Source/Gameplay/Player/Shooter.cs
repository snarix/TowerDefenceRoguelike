using System;
using DG.Tweening;
using TowerDefenceRoguelike.Gameplay.Base.Abstractions;
using TowerDefenceRoguelike.Gameplay.Player.Abstractions;
using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Player
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private ShooterRadiusFxView _shooterRadiusFxView;
        [SerializeField] private float _rotationTime = 0.3f;
        [SerializeField] private float _baseDelay = 3f;

        private IEnemyFinder _enemyFinder;
        private PlayerStats _playerStats;
        private float _nextAttackTime = 0f;
        
        public event Action<IDamageable> BloodSpawned;
        public event Action ShootSpawned;
        
        public void Initialize(IEnemyFinder enemyFinder, PlayerStats playerStats)
        {
            _enemyFinder = enemyFinder;
            _playerStats = playerStats;
            _shooterRadiusFxView.Initialize(playerStats);
        }

        private void Update()
        {
            if (Time.time < _nextAttackTime)
                return;

            var nearestEnemy = _enemyFinder.FindNearestEnemy(transform.position, _playerStats.Radius.Value);
            
            if (nearestEnemy != null)
            {
                transform.DOLookAt(nearestEnemy.Position, _rotationTime).OnComplete(() =>
                {
                    ShootSpawned?.Invoke();
                    nearestEnemy.TakeDamage(_playerStats.Damage.Value);
                    BloodSpawned?.Invoke(nearestEnemy);
                });
                _nextAttackTime = _baseDelay / _playerStats.Cooldown;
                _nextAttackTime += Time.time;
            }
        }
    }
}