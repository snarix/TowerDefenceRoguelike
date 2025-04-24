using System;
using System.Collections.Generic;
using _Source.Gameplay.BonusSystem.Data;
using _Source.Gameplay.Currency;
using _Source.Gameplay.UI.BonusView;
using _Source.MetaGameplay.MetaUpgrade;
using _Source.MetaGameplay.MetaUpgrade.Visitor;
using _Source.MetaGameplay.Transition;
using Include;
using TowerDefenceRoguelike.Gameplay.Player;
using UnityEngine;

namespace _Source.MetaGameplay
{
    public class MainMenuBootstrap : MonoBehaviour
    {
        [SerializeField] private MainMenuUIFacade _uiFacade;
        [SerializeField] private BonusConfigHolder _bonusConfig;
        [SerializeField] private UpgradeViewFactory _upgradeViewFactory;
        [SerializeField] private PlayerStatsConfig _playerStatsConfig;
        [SerializeField] private CurrencyValueConfig _currencyValueConfig;

        private List<IDisposable> _disposables = new List<IDisposable>();
        private IScenesLoadingService _scenesLoadingService;
        private IBonusViewFactory _bonusViewFactory;
        private ISaveLoadService _saveLoadService;
        private WalletDataLoader _walletDataLoader;
        private MetaWallet _metaWallet;
        private PlayerStatsDataLoader _playerStatsDataLoader;
        private PlayerStats _playerStats;
        private BonusPriceDataLoader _bonusPriceDataLoader;
        
        private void Awake()
        {
            _saveLoadService = new SaveLoadService();
            if (ServiceLocator.HasService<WalletDataLoader>() == false)
            {
                _walletDataLoader = new WalletDataLoader(_currencyValueConfig, _saveLoadService);
                ServiceLocator.AddService(_walletDataLoader);
            }
            
            if (ServiceLocator.HasService<MetaWallet>() == false)
            {
                _metaWallet = _walletDataLoader.LoadWallet();
                ServiceLocator.AddService(_metaWallet);
            }
            
            if (ServiceLocator.HasService<PlayerStatsDataLoader>() == false)
            {
                _playerStatsDataLoader = new PlayerStatsDataLoader(_playerStatsConfig, _saveLoadService);
                ServiceLocator.AddService(_playerStatsDataLoader);
            }
            
            if (ServiceLocator.HasService<BonusPriceDataLoader>() == false)
            {
                _bonusPriceDataLoader = new BonusPriceDataLoader(_bonusConfig, _saveLoadService);
                ServiceLocator.AddService(_bonusPriceDataLoader);
            }
        }

        private void Start()
        {
            _scenesLoadingService = new SceneLoadingService();
            
            _playerStatsDataLoader = ServiceLocator.GetService<PlayerStatsDataLoader>(); ;
            _playerStats = _playerStatsDataLoader.LoadPlayerStats();
            
            _bonusPriceDataLoader = ServiceLocator.GetService<BonusPriceDataLoader>();
            _bonusPriceDataLoader.LoadBonusPrice();

            _metaWallet = ServiceLocator.GetService<MetaWallet>();
            _uiFacade.GoldView.Initialize(_metaWallet.Gold);
            _uiFacade.MetaUpgradePanelView.Initialize(_upgradeViewFactory);

            var metaUpgradeApplier = new MetaUpgradeApplier(_bonusConfig, _playerStats, _uiFacade.MetaUpgradePanelView);
            _disposables.Add(metaUpgradeApplier);
        }

        private void OnEnable() => _uiFacade.PlayClicked += OnPlayClicked;

        private void OnDisable()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }

            _uiFacade.PlayClicked -= OnPlayClicked;
        }

        private void OnPlayClicked()
        {
            _scenesLoadingService.LoadWithData(SceneName.Gameplay, _playerStats);
        }
    }
}