using System;
using _Source.Gameplay.AbilitySystem.Abstractions;
using _Source.Gameplay.AbilitySystem.Data;
using _Source.Gameplay.UI.Ability;
using TowerDefenceRoguelike.Gameplay.Enemy.Factory;
using TowerDefenceRoguelike.Gameplay.Player;
using TowerDefenceRoguelike.Gameplay.Player.Abstractions;
using UnityEngine;

namespace _Source.Gameplay.AbilitySystem.Abilities
{
    public class DamageInArea : RestrictUsageCountAbility, IDisposable
    {
        private PlayerStats _playerStats;
        private ZoneChooser _zoneChooser;
        private EnemySpawner _spawner;
        private IEnemiesFinder _enemiesFinder;
        private DamageInAreaView _damageInAreaView;
        
        private float _radius;
        private bool _isInUse;
        
        public event Action<Vector3> SpawnedExplosion;
        
        public DamageInArea(AbilityData abilityData, int maximumUsageCount, float radius, IEnemiesFinder enemiesFinder, PlayerStats playerStats,
            ZoneChooser zoneChooser, EnemySpawner enemySpawner, DamageInAreaView damageInAreaView) : base(abilityData, maximumUsageCount)
        {
            _radius = radius;
            
            _playerStats = playerStats;
            _zoneChooser = zoneChooser;
            _spawner = enemySpawner;
            _enemiesFinder = enemiesFinder;
            _damageInAreaView = damageInAreaView;

            _spawner.WaveFinished += OnWaveFinished;
            _zoneChooser.ZoneCanceled += OnZoneCanceled;
        }

        public void Dispose()
        {
            _spawner.WaveFinished -= OnWaveFinished;
            _zoneChooser.ZoneCanceled -= OnZoneCanceled;
        }

        public override AbilityType Type => AbilityType.DamageInAreaAbility;

        protected override void OnUse()
        {
            if (_isInUse)
                throw new Exception("Can't use damage in area");
            
            _isInUse = true;
            _damageInAreaView.Show(_radius, _zoneChooser.GetCurrentWorldPosition());
            _zoneChooser.StartChoosing((zone) =>
            {
                _damageInAreaView.Hide();
                var enemiesInZone = _enemiesFinder.FindAllEnemies(zone, _radius);
                foreach (var enemy in enemiesInZone)
                {
                    enemy.TakeDamage(_playerStats.Damage.Value + _playerStats.Damage.Value);
                }
                SpawnedExplosion?.Invoke(zone);
                _isInUse = false;
            });
        }

        private void CancelZone()
        {
            _damageInAreaView.Hide();
            IncreaseUsageCount();
            _isInUse = false;
        }

        private void OnWaveFinished(Wave wave)
        {
            if (_isInUse)
                CancelZone();
        }
        
        private void OnZoneCanceled() => CancelZone();
    }
}