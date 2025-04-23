using System;
using _Source.Gameplay.UI;
using _Source.MetaGameplay.MetaUpgrade.UpgradeView;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.MetaGameplay
{
    public class MainMenuUIFacade : MonoBehaviour
    {
        [SerializeField] private Button _activateStartGameButton;
        [SerializeField] private Button _activateUpgradeButton;
        [SerializeField] private Button _activateShopButton;

        [SerializeField] private GameObject _startGameButtonEnable;
        [SerializeField] private GameObject _upgradeButtonEnable;
        [SerializeField] private GameObject _shopButtonEnable;

        [SerializeField] private GameObject _startGameMenu;
        [SerializeField] private GameObject _upgradeMenu;
        [SerializeField] private GameObject _abilityMenu;

        [SerializeField] private GameObject _menuBackground;
        
        [SerializeField] private Button _loadGameButton;
        
        [SerializeField] private MetaUpgradePanelView _metaUpgradePanelView;
        
        [SerializeField] private CurrencyView _goldView;
        
        public event Action PlayClicked;
        
        public CurrencyView GoldView => _goldView;
        public MetaUpgradePanelView MetaUpgradePanelView => _metaUpgradePanelView;
        
        private void OnEnable()
        {
            _loadGameButton.onClick.AddListener(Transition);
            
            _activateStartGameButton.onClick.AddListener(StartGame);
            _activateUpgradeButton.onClick.AddListener(Upgrade);
            _activateShopButton.onClick.AddListener(Shop);
        }

        private void OnDisable()
        {
            _loadGameButton.onClick.RemoveListener(Transition);
            
            _activateStartGameButton.onClick.RemoveListener(StartGame);
            _activateUpgradeButton.onClick.RemoveListener(Upgrade);
            _activateShopButton.onClick.RemoveListener(Shop);
        }
        
        private void Transition() => PlayClicked?.Invoke();
        
        private void StartGame()
        {
            _startGameMenu.SetActive(true);
            _startGameButtonEnable.SetActive(true);

            _upgradeMenu.SetActive(false);
            _upgradeButtonEnable.SetActive(false);
            _abilityMenu.SetActive(false);
            _shopButtonEnable.SetActive(false);
            _menuBackground.SetActive(false);
        }
        
        private void Upgrade()
        {
            _upgradeMenu.SetActive(true);
            _upgradeButtonEnable.SetActive(true);
            _menuBackground.SetActive(true);

            _startGameMenu.SetActive(false);
            _startGameButtonEnable.SetActive(false);
            _abilityMenu.SetActive(false);
            _shopButtonEnable.SetActive(false);
        }
        
        private void Shop()
        {
            _abilityMenu.SetActive(true);
            _shopButtonEnable.SetActive(true);
            _menuBackground.SetActive(true);
            
            _startGameMenu.SetActive(false);
            _startGameButtonEnable.SetActive(false);
            _upgradeMenu.SetActive(false);
            _upgradeButtonEnable.SetActive(false);
        }
    }
}