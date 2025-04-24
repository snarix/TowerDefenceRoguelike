using System;
using System.Collections.Generic;
using _Source.Gameplay.AbilitySystem;
using _Source.Gameplay.AbilitySystem.Data;
using _Source.Gameplay.Base;
using _Source.Gameplay.BonusSystem;
using _Source.Gameplay.BonusSystem.Data;
using _Source.Gameplay.Currency;
using _Source.Gameplay.UI;
using _Source.Gameplay.UI.Ability;
using _Source.Gameplay.UI.BonusView;
using Include;
using TowerDefenceRoguelike.Gameplay.Base;
using TowerDefenceRoguelike.Gameplay.Enemy.Factory;
using TowerDefenceRoguelike.Gameplay.Player.EnemyFinder;
using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Player
{
    [DefaultExecutionOrder(1000)]
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private Health _attackTarget;
        [SerializeField] private Player _player;
        [SerializeField] private UIFacade _uiFacade;
        [SerializeField] private RewardConfig _rewardConfig;
        [SerializeField] private BonusConfigHolder _bonusConfig;
        [SerializeField] private BonusViewFactory _bonusViewFactory;

        [SerializeField] private AbilityConfigHolder _abilityConfigHolder;
        [SerializeField] private AbilityButtonFactory _abilityButtonFactory;
        [SerializeField] private MultiplierConfig _multiplierConfig;
        [SerializeField] private ConfigWallet _configWallet;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private SpawnData _spawnData;
        
        private EnemySpawner _enemySpawner;
        private List<IDisposable> _disposables = new List<IDisposable>();
        private GameplayController _gameplayController;
        private RewardGiver _rewardGiver;
        private EnemyFinderInArea _enemiesFinder;
        private ZoneChooser _zoneChooser;
        private IScenesLoadingService _scenesLoadingService;
        private Currency _gold;
        private GameplayWallet _gameplayWallet;
        private MetaWallet _metaWallet;
        private GameSessionController _gameSessionController;

        private void Start()
        {
            _scenesLoadingService = new SceneLoadingService();

            var data = ServiceLocator.GetService<SceneTransitionData<PlayerStats>>();
            var playerData = data.Data;

            _uiFacade.BonusPanelView.Initialize(_bonusViewFactory);
            _uiFacade.AbilityView.Initialize(_abilityButtonFactory);

            _metaWallet = ServiceLocator.GetService<MetaWallet>();
            var dollar = new Currency(_configWallet.Dollar);
            _gold = new Currency(_configWallet.Gold);
            _gameplayWallet = new GameplayWallet(dollar, _gold);
            
            ServiceLocator.RemoveService<GameplayWallet>();
            ServiceLocator.AddService(_gameplayWallet);
            
            _uiFacade.DollarView.Initialize(_gameplayWallet.Dollar);
            _uiFacade.GoldView.Initialize(_gameplayWallet.Gold);

            _player.Initialize(playerData, _multiplierConfig);

            var enemyFactory = new EnemyFactory();
            var coroutineHandler = new MonoBehaviourCoroutineHandler(this);

            _enemySpawner = new EnemySpawner(enemyFactory, coroutineHandler, _spawnData, _attackTarget);

            _rewardGiver = new RewardGiver(_enemySpawner, _gameplayWallet, _rewardConfig, playerData);

            var bonusApplier = new TestBonusApplier(_bonusConfig, playerData, _uiFacade.BonusPanelView);

            _gameplayController = new GameplayController(_spawnData, _enemySpawner, coroutineHandler, _uiFacade, bonusApplier);

            _enemiesFinder = new EnemyFinderInArea(_enemyLayer);
            var inputSystem = new InputSystem();

            _zoneChooser = new ZoneChooser(Camera.main, inputSystem);

            var abilityController = new AbilityController(playerData, _abilityConfigHolder, _uiFacade.AbilityView, coroutineHandler,
                _enemiesFinder, _uiFacade.DamageInAreaView, _enemySpawner, _zoneChooser, _uiFacade.DamageInAreaParticleView);
            abilityController.CreateAbility();

            _uiFacade.Initialize(_player, _rewardGiver, _zoneChooser, playerData);

            _gameSessionController = new GameSessionController(_uiFacade, _gameplayWallet);

            _disposables.Add(_enemySpawner);
            _disposables.Add(_gameplayController);
            _disposables.Add(_rewardGiver);
            _disposables.Add(bonusApplier);
            _disposables.Add(abilityController);
            _disposables.Add(_zoneChooser);
            _disposables.Add(inputSystem);
            _disposables.Add(_gameSessionController);

            _gameplayController.StartWave();
        }

        private void OnDestroy()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}