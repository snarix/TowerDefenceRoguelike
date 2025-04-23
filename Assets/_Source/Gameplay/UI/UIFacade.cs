using System;
using System.Collections.Generic;
using _Source.Gameplay.AbilitySystem;
using _Source.Gameplay.Currency;
using _Source.Gameplay.UI.Ability;
using _Source.Gameplay.UI.BonusView;
using DG.Tweening;
using Include;
using UnityEngine;
using TMPro;
using TowerDefenceRoguelike.Gameplay.Player;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;
using I2.Loc;

namespace _Source.Gameplay.UI
{
    public class UIFacade : MonoBehaviour
    {
        [SerializeField] private HealthView.HealthView _playerImageHealthView;
        [SerializeField] private HealthView.HealthView _playerTextHealthView;

        [SerializeField] private CountEnemyText _countEnemyText;
        [SerializeField] private TextMeshProUGUI _currentWaveText;

        [SerializeField] private CurrencyView _dollarView;
        [SerializeField] private CurrencyView _goldView;
        
        [SerializeField] private BonusPanelView _bonusPanelView;
        
        [SerializeField] private RewardView _dollarRewardView;
        [SerializeField] private RewardView _goldRewardView;
        
        [SerializeField] private List<WaveTextAnimation>  _rewardForWaveTextAnimation;
        
        [SerializeField] private AbilityView _abilityView;
        
        [SerializeField] private DamageInAreaParticleView _damageInAreaParticleView;
        [SerializeField] private DamageInAreaView _damageInAreaView;
        
        [SerializeField] private ExitPanelView _exitPanelView;
        [SerializeField] private Button _exitButton;
        
        [SerializeField] private PlayerStatsTextView _playerStatsTextView;
        [SerializeField] private EnemyStatsTextView _enemyStatsTextView;

        private Sequence _sequence;
        private Player _player;

        public CurrencyView DollarView => _dollarView;
        public CurrencyView GoldView => _goldView;

        public BonusPanelView BonusPanelView => _bonusPanelView;

        public DamageInAreaParticleView DamageInAreaParticleView => _damageInAreaParticleView;
        public DamageInAreaView DamageInAreaView => _damageInAreaView;

        public AbilityView AbilityView => _abilityView;

        public event Action ToBaseClicked;
        public event Action RetryClicked;
        
        public void Initialize(Player player, RewardGiver rewardGiver, ZoneChooser zoneChooser, PlayerStats playerStats)
        {
            _player = player;
            _playerImageHealthView.Initialize(player.Health);
            _playerTextHealthView.Initialize(player.Health);
            
            _dollarRewardView.Initialize(rewardGiver);
            _goldRewardView.Initialize(rewardGiver);
            
            _damageInAreaView.Initialize(zoneChooser);
            
            _playerStatsTextView.UpdateDamageText(playerStats.Damage.Value);
            _playerStatsTextView.UpdateHealthRegenerationText(playerStats.HealthRegeneration.Value);
            player.HealthRegenerationValueChanged += OnHealthRegenerationValueChanged;
            player.Shooter.DamageValueChanged += OnDamageValueChanged;

            _exitButton.onClick.AddListener(OnExitClicked);
            
            _exitPanelView.YesClicked += OnYesClicked;
            _exitPanelView.NoClicked += OnNoClicked;
            _exitPanelView.RetryClicked += OnRetryClicked;
            _exitPanelView.ToBaseClicked += OnToBaseClicked;
            
            player.Died += OnPlayerDied;
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
            
            _exitButton.onClick.RemoveListener(OnExitClicked);
            
            _player.HealthRegenerationValueChanged += OnHealthRegenerationValueChanged;
            _player.Shooter.DamageValueChanged += OnDamageValueChanged;
            
            _exitPanelView.YesClicked -= OnYesClicked;
            _exitPanelView.NoClicked -= OnNoClicked;
            _exitPanelView.RetryClicked -= OnRetryClicked;
            _exitPanelView.ToBaseClicked -= OnToBaseClicked;
            
            _player.Died += OnPlayerDied;
        }

        public void UpdateEnemyCount(int count) => _countEnemyText.UpdateEnemyCount(count);

        public void UpdateCurrentWaveText(int currentWave) => _currentWaveText.text = $"{LocalizationManager.GetTranslation("UiWave")} - {currentWave + 1}";
        
        private void OnDamageValueChanged(int damage) => _playerStatsTextView.UpdateDamageText(damage);

        private void OnHealthRegenerationValueChanged(float healthRegeneration) => _playerStatsTextView.UpdateHealthRegenerationText(healthRegeneration);
        
        public void UpdateHealthText(int maxHealth) => _enemyStatsTextView.UpdateHealthText(maxHealth);

        public void ShowRewardForWave()
        {
            foreach (var rewardWaveText in _rewardForWaveTextAnimation)
            {
                rewardWaveText.Show();
                rewardWaveText.gameObject.SetActive(true);
            }
        }
        
        public void HideRewardForWave()
        {
            foreach (var rewardWaveText in _rewardForWaveTextAnimation)
                rewardWaveText.Hide();
        }
        
        private void OnExitClicked()
        {
            bool isPanelActive = _exitPanelView.AreYouSurePanel.activeSelf;
            _exitPanelView.AreYouSurePanel.SetActive(!isPanelActive);
            
            Time.timeScale = isPanelActive ? 1 : 0;
        }
        
        private void OnYesClicked()
        {
            Time.timeScale = 0;
            _exitPanelView.AreYouSurePanel.SetActive(false);
            _exitPanelView.ScreenDeathPanel.SetActive(true);
            var wallet = ServiceLocator.GetService<GameplayWallet>();
            _exitPanelView.Initialize((int)wallet.Gold.Value);
        }

        private void OnNoClicked()
        {
            Time.timeScale = 1;    
            _exitPanelView.AreYouSurePanel.SetActive(false);
        } 
        
        private void OnPlayerDied() => OnYesClicked();

        private void OnToBaseClicked()
        {
            Time.timeScale = 1;
            ToBaseClicked?.Invoke();
        }

        private void OnRetryClicked()
        {
            Time.timeScale = 1;    
            RetryClicked?.Invoke();
        } 
    }
}