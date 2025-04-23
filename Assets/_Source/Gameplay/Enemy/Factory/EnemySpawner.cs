using System;
using System.Collections;
using System.Collections.Generic;
using _Source.Gameplay.Currency;
using Include;
using TowerDefenceRoguelike.Gameplay.Base;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TowerDefenceRoguelike.Gameplay.Enemy.Factory
{
    public class EnemySpawner : IDisposable
    {
        private IEnemyFactory _enemyFactory;
        private ICoroutineHandler _coroutineHandler;
        private SpawnData _spawnData;
        private Health _player;
        private List<Enemy> _enemies = new List<Enemy>();

        private int _enemiesRemaining;
        private int _currentWaveIndex = 0;
        private bool _isSpawnFinished = false;
        
        public event Action<int> EnemySpawned; 
        public event Action<int> EnemyHealthUpdated;
        
        public event Action<Enemy> EnemyDied;
        public event Action<int> EnemyAmountUpdated;

        public event Action<Wave, int> WaveStarted;
        public event Action<Wave> WaveFinished;

        public event Action<int> CurrentWaveIndexChanged;

        private bool IsWavesEnded => _currentWaveIndex >= _spawnData.Waves.Count;

        public EnemySpawner(IEnemyFactory enemyFactory, ICoroutineHandler coroutineHandler, SpawnData spawnData,
            Health player)
        {
            _enemyFactory = enemyFactory;
            _coroutineHandler = coroutineHandler;
            _spawnData = spawnData;
            _player = player;
        }

        public void StartFirstWave()
        {
            _coroutineHandler.StartCoroutine(SpawnWaveRoutine());
        }

        private IEnumerator SpawnWaveRoutine()
        {
            if (IsWavesEnded)
            {
                Debug.Log("Gameplay Ended");
                yield break;
            }

            _isSpawnFinished = false;
            Debug.Log("Wave Spawning started");

            int enemiesToSpawn = _spawnData.Waves[_currentWaveIndex].EnemiesPerWave.Count;
            _enemiesRemaining = enemiesToSpawn;
            WaveStarted?.Invoke(_spawnData.Waves[_currentWaveIndex], _currentWaveIndex);
            
            foreach (var enemyPrefab in _spawnData.Waves[_currentWaveIndex].EnemiesPerWave)
            {
                var spawnPoint = _spawnData.SpawnPoints[Random.Range(0, _spawnData.SpawnPoints.Count)];

                SpawnEnemy(spawnPoint.position, enemyPrefab);
                yield return new WaitForSeconds(_spawnData.Waves[_currentWaveIndex].TimeBetweenSpawn);
            }

            _isSpawnFinished = true;

            Debug.Log("Wave spawning finished");
        }
        
        private void SpawnEnemy(Vector3 spawnPosition, Enemy enemyPrefab)
        {
            var enemy = _enemyFactory.CreateEnemy(spawnPosition, enemyPrefab);
            _enemies.Add(enemy);
            enemy.Initialize(_player, this);
            
            EnemySpawned?.Invoke(_currentWaveIndex);
            enemy.EnemyHealthUpdated += OnEnemyHealthUpdated;
            
            enemy.Died += OnEnemyDied;
        }

        private void OnEnemyHealthUpdated(int newHealth)
        {
            EnemyHealthUpdated?.Invoke(newHealth);
        }

        private void OnEnemyDied(Enemy enemyHealth)
        {
            _enemies.Remove(enemyHealth);
            enemyHealth.Died -= OnEnemyDied;

            _enemiesRemaining--;
            EnemyAmountUpdated?.Invoke(_enemiesRemaining);
            EnemyDied?.Invoke(enemyHealth);

            if (_isSpawnFinished && _enemies.Count == 0)
            {
                Debug.Log("Wave finished");
                _isSpawnFinished = false;

                WaveFinished?.Invoke(_spawnData.Waves[_currentWaveIndex]);
                
                _currentWaveIndex++;
                CurrentWaveIndexChanged?.Invoke(_currentWaveIndex);
            }
        }

        public void Dispose()
        {
            foreach (var health in _enemies)
            {
                health.Died -= OnEnemyDied;
                health.EnemyHealthUpdated -= OnEnemyHealthUpdated;
            }
        }
    }
}