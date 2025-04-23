using System;
using System.Collections;
using _Source.Gameplay.BonusSystem;
using _Source.Gameplay.Currency;
using _Source.Gameplay.UI;
using Include;
using TowerDefenceRoguelike.Gameplay.Enemy;
using TowerDefenceRoguelike.Gameplay.Enemy.Factory;
using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Player
{
    public class GameplayController : IDisposable
    {
        private SpawnData _spawnData;
        private EnemySpawner _enemySpawner;
        private ICoroutineHandler _coroutineHandler;
        private UIFacade _uiFacade;
        private TestBonusApplier _testBonusApplier;

        public GameplayController(SpawnData spawnData, EnemySpawner enemySpawner, ICoroutineHandler coroutineHandler,
            UIFacade uiFacade, TestBonusApplier testBonusApplier)
        {
            _spawnData = spawnData;
            _enemySpawner = enemySpawner;
            _coroutineHandler = coroutineHandler;
            _uiFacade = uiFacade;
            _testBonusApplier = testBonusApplier;
     
            _enemySpawner.WaveStarted += OnWaveStarted;
            _enemySpawner.WaveFinished += OnWaveFinished;
            _enemySpawner.EnemyAmountUpdated += OnEnemyAmountUpdated;
            _enemySpawner.EnemyHealthUpdated += OnEnemyHealthUpdated;
            
            _testBonusApplier.Confirmed += OnConfirmed;
        }

        public void StartWave()
        {
            //_uiFacade.HideBonus();
            _enemySpawner.StartFirstWave();
            //_healthRegeneration.Activate();
        }

        public void Dispose()
        {
            _enemySpawner.WaveStarted -= OnWaveStarted;
            _enemySpawner.WaveFinished -= OnWaveFinished;
            _enemySpawner.EnemyAmountUpdated -= OnEnemyAmountUpdated;
            _enemySpawner.EnemyHealthUpdated -= OnEnemyHealthUpdated;
            
            _testBonusApplier.Confirmed -= OnConfirmed;
        }

        private void OnEnemyHealthUpdated(int newHealth)
        {
            _uiFacade.UpdateHealthText(newHealth);
        }

        private void OnWaveStarted(Wave wave,int currentWaveIndex)
        {
            _uiFacade.UpdateEnemyCount(wave.EnemiesPerWave.Count);
            _uiFacade.UpdateCurrentWaveText(currentWaveIndex);
        }

        private void OnWaveFinished(Wave wave)
        {
            _coroutineHandler.StartCoroutine(WaveFinishedRoutine());
           // _healthRegeneration.Deactivate();
        }
        
        private void OnConfirmed()
        {
            _coroutineHandler.StartCoroutine(StartNextWaveRoutine()); //?
            _testBonusApplier.HideConfirmReRollButton();
            _testBonusApplier.DestroyBonuses();
            _uiFacade.AbilityView.ShowPanelAbility();
        }
        
        private void OnEnemyAmountUpdated(int enemiesRemaining)
        {
            _uiFacade.UpdateEnemyCount(enemiesRemaining);
        }

        private IEnumerator StartNextWaveRoutine()
        {
            yield return new WaitForSeconds(_spawnData.TimeBetweenWaves);

            StartWave();
        }

        private IEnumerator WaveFinishedRoutine()
        {
            _uiFacade.ShowRewardForWave();
            yield return new WaitForSeconds(0.5f);
            _testBonusApplier.ShowBonuses();
            _testBonusApplier.ShowConfirmReRollButton();
            _uiFacade.AbilityView.HidePanelAbility();
            _uiFacade.HideRewardForWave();
        }
    }
}