using System;
using _Source.Gameplay.Currency;
using TowerDefenceRoguelike.Gameplay.Enemy.Factory;
using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Player
{
    public class RewardGiver : IDisposable
    {
        private RewardConfig _rewardConfig;
        private EnemySpawner _enemySpawner;
        private GameplayWallet _gameplayWallet;
        private PlayerStats _playerStats;
        
        public event Action<float, float> RewardedForWave;

        public RewardGiver(EnemySpawner enemySpawner, GameplayWallet gameplayWallet, RewardConfig rewardConfig, PlayerStats playerStats)
        {
            _enemySpawner = enemySpawner;
            _gameplayWallet = gameplayWallet;
            _rewardConfig = rewardConfig;
            _playerStats = playerStats;
                
            _enemySpawner.EnemyDied += OnEnemyDied;
            _enemySpawner.WaveFinished += OnWaveFinished;
        }
        
        public void Dispose()
        {
            _enemySpawner.EnemyDied -= OnEnemyDied;
            _enemySpawner.WaveFinished -= OnWaveFinished;
        }
        
        private void OnWaveFinished(Wave wave)
        {
            var dollarReward = wave.Reward.Dollar * _playerStats.DollarForWaveMultiplier;
            var goldReward = wave.Reward.Gold * _playerStats.GoldForWaveMultiplier;
            
            _gameplayWallet.Dollar.Add(dollarReward);
            _gameplayWallet.Gold.Add(goldReward);
            
            RewardedForWave?.Invoke(dollarReward, goldReward);
            
            Debug.Log($"Всего Долларов: {_gameplayWallet.Dollar.Value}." +
                      $"\nВсего монет: {_gameplayWallet.Gold.Value}");
        }

        private void OnEnemyDied(Enemy.Enemy enemy)
        {
            var reward = _rewardConfig.GetReward(enemy.Type);
            
            var dollarReward = reward.Dollar * _playerStats.DollarForKillingMultiplier;
            var goldReward = reward.Gold * _playerStats.GoldForKillingMultiplier;
            
            _gameplayWallet.Dollar.Add(dollarReward);
            _gameplayWallet.Gold.Add(goldReward);
        }
    }
}