using System.Collections.Generic;
using _Source.Gameplay.BonusSystem;
using _Source.Gameplay.BonusSystem.Abstractions;
using _Source.Gameplay.BonusSystem.Data;
using _Source.Gameplay.Currency;
using DG.Tweening;
using Include;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

namespace _Source.Gameplay.UI.BonusView
{
    public class PlayerStatsBonusView : BonusView
    {
        [SerializeField] private Image _bonusIcon;
        [SerializeField] private TextMeshProUGUI _bonusNameText;
        [SerializeField] private TextMeshProUGUI _bonusDescriptionText;

        [SerializeField] private TextMeshProUGUI _inititalValueStatText;
        [SerializeField] private TextMeshProUGUI _endValueStatText;
        [SerializeField] private TextMeshProUGUI _dollarPrice;

        [SerializeField] private Image _buyImage;
        [SerializeField] private Color _enoughImageColor;
        [SerializeField] private Color _enoughTextColor;
        [SerializeField] private Color _notEnoughImageColor;
        [SerializeField] private Color _notEnoughTextColor;

        [SerializeField] private Image _backgroundImage;
        [SerializeField] private List<CanvasGroup> _contentElements;

        [SerializeField] private float _maxAlpha = 1f;
        [SerializeField] private float _minAlpha = 1f;
        [SerializeField] private float _fadeDuration = 0.3f;

        private Sequence _fadeSequence;
        
        private PlayerStatsBonus _statsBonus;
        private int _dollar;

        public void Initialize(BonusType bonusType, string bonusName, string bonusDescription, float currentValue,
            float nextValue, Image bonusIcon, int dollarPrice, BonusData bonusData, PlayerStatsBonus statsBonus)
        {
            _bonusType = bonusType;

            _bonusNameText.text = bonusName;
            _bonusDescriptionText.text = bonusDescription;
            
            _inititalValueStatText.text = FormatValue(currentValue, bonusData);
            _endValueStatText.text = FormatValue(nextValue, bonusData);
            
            _bonusIcon.sprite = bonusIcon.sprite;

            _dollar = dollarPrice;
            _dollarPrice.text = $"${dollarPrice}";
            
            _statsBonus = statsBonus;
        }

        private void Update()
        {
            if (_statsBonus != null)
            {
                _inititalValueStatText.text = FormatValue(_statsBonus.GetCurrentValue(), _statsBonus.BonusData);
                _endValueStatText.text = FormatValue(_statsBonus.GetNextValue(), _statsBonus.BonusData);
                _dollarPrice.text = $"${_statsBonus.BonusData.BonusDollarPrice}";
            }
            UpdateButtonColor();
        }
        
        private void OnDestroy() => _fadeSequence?.Kill();
        
        private void UpdateButtonColor()
        {
            var wallet = ServiceLocator.GetService<GameplayWallet>();
            if (wallet.Dollar.IsEnough(_dollar))
            {
                Unlock();
                _buyImage.color = _enoughImageColor;
                _dollarPrice.color = _enoughTextColor;
            }
            else
            {
                Lock();
                _buyImage.color = _notEnoughImageColor;
                _dollarPrice.color = _notEnoughTextColor;
            }
        }

        public void ActivateVoid()
        {
            _fadeSequence?.Kill();
            _fadeSequence = DOTween.Sequence();

            _fadeSequence.Append(_backgroundImage.DOFade(_maxAlpha, _fadeDuration));

            foreach (var element in _contentElements)
            {
                _fadeSequence.Join(element.DOFade(_minAlpha, 0f));
            }
            _fadeSequence.OnComplete(() => _bonusButton.interactable = false);
        }
    }
}