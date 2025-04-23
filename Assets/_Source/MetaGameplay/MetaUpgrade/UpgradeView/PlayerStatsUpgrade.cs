using _Source.Gameplay.BonusSystem;
using _Source.Gameplay.BonusSystem.Abstractions;
using _Source.Gameplay.BonusSystem.Data;
using _Source.Gameplay.Currency;
using _Source.Gameplay.UI.BonusView;
using Include;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Source.MetaGameplay.MetaUpgrade.UpgradeView
{
    public class PlayerStatsUpgrade : BonusView
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _inititalValueStat;
        [SerializeField] private TextMeshProUGUI _endValueStat;
        [SerializeField] private TextMeshProUGUI _goldPrice;
        
        [SerializeField] private Image _buyImage;
        [SerializeField] private Color _enoughImageColor;
        [SerializeField] private Color _enoughTextColor;
        [SerializeField] private Color _notEnoughImageColor;
        [SerializeField] private Color _notEnoughTextColor;
        
        private PlayerStatsBonus _statsBonus;
        private int _gold;
        
        public void Initialize(BonusType bonusType, string bonusName, float currentValue, float nextValue, Image bonusIcon, int goldPrice, BonusData bonusData, PlayerStatsBonus statsBonus)
        {
            _bonusType = bonusType;
            
            _name.text = bonusName;
            _inititalValueStat.text = FormatValue(currentValue, bonusData);
            _endValueStat.text = FormatValue(nextValue, bonusData);
            _image.sprite = bonusIcon.sprite;
            _gold = goldPrice;
            _goldPrice.text = goldPrice.ToString();
            
            _statsBonus = statsBonus;
        }

        private void Update()
        {
            if (_statsBonus != null)
            {
                _inititalValueStat.text = FormatValue(_statsBonus.GetCurrentValue(), _statsBonus.BonusData);
                _endValueStat.text = FormatValue(_statsBonus.GetNextValue(), _statsBonus.BonusData);
                _goldPrice.text = _statsBonus.BonusData.BonusGoldPrice.ToString();
                UpdateButtonColor();
            }
        }

        private void UpdateButtonColor()
        {
            var wallet = ServiceLocator.GetService<MetaWallet>();
            if (wallet.Gold.IsEnough(_gold))
            {
                Unlock();
                _buyImage.color = _enoughImageColor;
                _goldPrice.color = _enoughTextColor;
            }
            else
            {
                Lock();
                _buyImage.color = _notEnoughImageColor;
                _goldPrice.color = _notEnoughTextColor;
            }
        }
    }
}