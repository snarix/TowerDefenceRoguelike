using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Source.Gameplay.UI
{
    public class CurrencyView : MonoBehaviour
    { 
        [SerializeField] private TextMeshProUGUI _currencyCountText;
        [SerializeField] private float _duration = 1f;
        
        private Currency.Currency _currency;
        private Tween _tween;
        
        private float _currentValue;

        public void Initialize(Currency.Currency currency)
        {
            _currency = currency;
            _currentValue = currency.Value;
            _currencyCountText.text = _currentValue.ToString("N0");
            _currency.BalanceChange += UpdateCurrency;
        }

        private void OnDestroy()
        {
            _currency.BalanceChange -= UpdateCurrency;
            _tween?.Kill();
        }

        private void UpdateCurrency(float endValue)
        {
            _tween?.Kill();
            
            _tween = DOTween.To(() => _currentValue, x => 
            {
                var roundToInt = Mathf.RoundToInt(_currentValue = x);
                _currencyCountText.text = roundToInt.ToString();
            }, endValue, _duration);
        }
    }
}