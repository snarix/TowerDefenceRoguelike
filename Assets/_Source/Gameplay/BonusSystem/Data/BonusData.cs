using UnityEngine;
using UnityEngine.UI;
using I2.Loc;

namespace _Source.Gameplay.BonusSystem.Data
{
    public abstract class BonusData : ScriptableObject
    {
        [SerializeField] private BonusType _bonusType;
        [SerializeField] private Image _bonusImage;
        [SerializeField] private string _bonusName; 
        [SerializeField] private string _bonusAlternativeName; 
        [SerializeField] private string _bonusDescriptionFormat;
        
        [SerializeField] private int _baseGoldPrice;
        [SerializeField] private int _baseDollarPrice; 
        [SerializeField] private int _amountDollarPrice;
        [SerializeField] private int _amountGoldPrice;
        
        private int _currentDollarPrice;
        private int _currentGoldPrice;

        public BonusType BonusType => _bonusType;
        
        public string BonusName => LocalizationManager.GetTranslation(_bonusName);

        public string BonusAlternativeName => LocalizationManager.GetTranslation(_bonusAlternativeName);

        public string BonusDescriptionFormat => LocalizationManager.GetTranslation(_bonusDescriptionFormat);
        
        public Image BonusImage => _bonusImage;

        public int BonusDollarPrice => _currentDollarPrice;
        public int BonusGoldPrice => _currentGoldPrice;
        
        public void InitializePrice()
        {
            _currentGoldPrice = _baseGoldPrice;
            _currentDollarPrice = _baseDollarPrice;
        }
        
        public void IncreaseGoldPrice()
        {
            _currentGoldPrice += _amountDollarPrice;
        }
        
        public void IncreaseDollarPrice()
        {
            _currentDollarPrice += _amountGoldPrice;
        }

        public void SetPrice(int newPrice)
        {
            _currentGoldPrice = newPrice;
        }
        
        public void ResetToGameplayPrice()
        {
            _currentDollarPrice = _baseDollarPrice;
        }
    }
}