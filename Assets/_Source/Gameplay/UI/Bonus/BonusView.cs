using System;
using _Source.Gameplay.BonusSystem;
using _Source.Gameplay.BonusSystem.Data;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.Gameplay.UI.BonusView
{
    public abstract class BonusView : MonoBehaviour
    {
        [SerializeField] protected Button _bonusButton;
        [SerializeField] private float _lockAnimationDuration = 0.4f;
        [SerializeField] private float _lockAnimationStrenght = 2f;
        
        private bool _isLocked;
        public BonusType BonusType { get; protected set; }

        public event Action<BonusType> BonusApplied;
        
        private void OnEnable() => _bonusButton.onClick.AddListener(OnButtonClick);

        private void OnDisable() => _bonusButton.onClick.RemoveListener(OnButtonClick);

        protected void Lock() => _isLocked = true;

        protected void Unlock() => _isLocked = false;

        private void OnButtonClick()
        {
            if (!_bonusButton.interactable) return;

            if (_isLocked)
            {
                transform.DOShakePosition(_lockAnimationDuration, _lockAnimationStrenght);
                return;
            }
            
            BonusApplied?.Invoke(BonusType);
        }

        protected string FormatValue(float value, BonusData bonus)
        {
            if (bonus is IntBonusData)
            {
                return value.ToString("N0");
            }
            if (bonus.BonusType == BonusType.CooldownBonus)
            {
                return value.ToString("F2");
            }
            return value.ToString("F1");
        }
    }
}