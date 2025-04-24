using System;
using _Source.Gameplay.Currency;
using _Source.Gameplay.UI;
using _Source.MetaGameplay;
using _Source.MetaGameplay.Transition;
using Include;

namespace TowerDefenceRoguelike.Gameplay.Player
{
    public class GameSessionController : IDisposable
    {
        private UIFacade _uiFacade;
        private GameplayWallet _gameplayWallet;
        private IScenesLoadingService _scenesLoadingService;
        private MetaWallet _metaWallet;

        public GameSessionController(UIFacade uiFacade, GameplayWallet gameplayWallet)
        {
            _uiFacade = uiFacade;
            _gameplayWallet = gameplayWallet;
            _scenesLoadingService = new SceneLoadingService();
            _metaWallet = ServiceLocator.GetService<MetaWallet>();
            
            _uiFacade.ToBaseClicked += OnMenuButtonClicked;
            _uiFacade.RetryClicked += OnRetryClicked;
        }
        
        public void Dispose()
        {
            _uiFacade.ToBaseClicked -= OnMenuButtonClicked;
            _uiFacade.RetryClicked -= OnRetryClicked;
        }
        
        private void SaveGold()
        {
            _metaWallet.Gold.Add(_gameplayWallet.Gold.Value);
            var walletDataLoader = ServiceLocator.GetService<WalletDataLoader>();
            walletDataLoader.SaveWallet(_metaWallet);
        }
        
        private void OnMenuButtonClicked()
        {
            SaveGold();
            _scenesLoadingService.Load(SceneName.MainMenu);
        }
        
        private void OnRetryClicked()
        {
            SaveGold();
            var playerStats = ServiceLocator.GetService<PlayerStatsDataLoader>();
            _scenesLoadingService.LoadWithData(SceneName.Gameplay, playerStats.LoadPlayerStats());
        }
    }
}