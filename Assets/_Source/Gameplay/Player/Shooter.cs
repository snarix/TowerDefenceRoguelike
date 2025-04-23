using System;
using DG.Tweening;
using TowerDefenceRoguelike.Gameplay.Player.Abstractions;
using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Player
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private float _rotationTime = 0.3f;
        [SerializeField] private float _baseDelay = 3f;
        [SerializeField] private ParticleSystem _shootFx;
        [SerializeField] private Transform _shootPosition;
        [SerializeField] private ParticleSystem _bloodFx;
        
        [SerializeField] private ParticleSystem _radiusCirclePrefab;

        private ParticleSystem _radiusRing;

        private IEnemyFinder _enemyFinder;
        private PlayerStats _playerStats;
        private float _nextAttackTime = 0f;

        public event Action<int> DamageValueChanged; 
        
        public void Initialize(IEnemyFinder enemyFinder, PlayerStats playerStats)
        {
            _enemyFinder = enemyFinder;
            _playerStats = playerStats;
            
            _radiusRing = Instantiate(_radiusCirclePrefab, transform.position + new Vector3(0f, -2.3f, 0f), Quaternion.Euler(90, 0, 0));
            _radiusRing.Play();
            
            _playerStats.Damage.OnValueChanged += DamageOnValueChanged;
            _playerStats.Radius.OnValueChanged += RadiusOnValueChanged;
            UpdateRadiusRing();
        }

        private void OnDestroy()
        {
            if (_playerStats != null)
            {
                _playerStats.Damage.OnValueChanged -= DamageOnValueChanged;
                _playerStats.Radius.OnValueChanged -= RadiusOnValueChanged;
            }
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
                    var shoot = Instantiate(_shootFx, _shootPosition.position, _shootPosition.rotation);
                    shoot.Play();
                    nearestEnemy.TakeDamage(_playerStats.Damage.Value);
                    var blood = Instantiate(_bloodFx, nearestEnemy.Position, Quaternion.identity);
                    blood.Play();
                });
                _nextAttackTime = _baseDelay / _playerStats.Cooldown;
                Debug.Log($"Attack Speed - {_nextAttackTime}");
                _nextAttackTime += Time.time;
                Debug.Log($"Next Attack Time - {_nextAttackTime}");
            }
        }

        private void OnDrawGizmos()
        {
            if(_playerStats == null) return;
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _playerStats.Radius.Value);
        }
        
        private void UpdateRadiusRing()
        {
            var shape = _radiusRing.shape;
            shape.radius = _playerStats.Radius.Value;
            //_radiusRing.transform.position = transform.position + new Vector3(0f, -2.3f, 0f);

            _radiusRing.Clear();
            _radiusRing.Play();
        }
        
        private void RadiusOnValueChanged(float radius)
        {
            UpdateRadiusRing();
        }
        
        private void DamageOnValueChanged(int damage)
        {
            DamageValueChanged?.Invoke(damage);
        }
    }
}