using System;
using System.Collections.Generic;
using _Source.Gameplay.BonusSystem;
using _Source.Gameplay.BonusSystem.Abstractions;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

namespace _Source.Gameplay.UI.BonusView
{
    public class BonusPanelView : MonoBehaviour
    {
        [SerializeField] private GameObject _bonusPanel;
        [SerializeField] private GameObject _backgroundPanel;
        [SerializeField] private ShakePositionAnimation _shakePositionAnimation;

        [SerializeField] private Button _reRollButton;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private int _amount;
        [SerializeField] private float _duration = 0.3f;
        [SerializeField] private int _currentPrice = 10;
        private int _changePrice;
        
        private Tween _tween;
        private PostProcessVolume _postProcessVolume;

        private IBonusViewFactory _bonusViewFactory;
        private List<BonusView> _bonusViews = new List<BonusView>();

        public int ChangePrice => _changePrice;

        public event Action<BonusType> BonusApplied;
        public event Action<ShakePositionAnimation> ReRolled;
        public event Action Confirmed;

        public void Initialize(IBonusViewFactory bonusViewFactory)
        {
            _bonusViewFactory = bonusViewFactory;

            _postProcessVolume = Camera.main?.gameObject.GetComponent<PostProcessVolume>();
            _reRollButton.onClick.AddListener(ReRoll);
            _confirmButton.onClick.AddListener(Confirm);
            ResetPrice();
        }

        private void OnDestroy()
        {
            _tween?.Kill();
            _reRollButton.onClick.RemoveListener(ReRoll);
            _confirmButton.onClick.RemoveListener(Confirm);
        }
        
        public void ShowConfirmReRollButton()
        {
            _confirmButton.gameObject.SetActive(true);
            _reRollButton.gameObject.SetActive(true);
        }

        public void HideConfirmReRollButton()
        {
            _confirmButton.gameObject.SetActive(false);
            _reRollButton.gameObject.SetActive(false);
        }

        public void ShowBonuses(IEnumerable<IBonus> bonuses)
        {
            _bonusPanel.SetActive(true);
            _postProcessVolume.enabled = true;

            foreach (var bonus in bonuses)
            {
                var bonusView = _bonusViewFactory.Create(bonus, _backgroundPanel.transform);
                bonusView.BonusApplied += OnBonusApplied;
                _bonusViews.Add(bonusView);
            }
        }

        public void DestroyBonuses()
        {
            _bonusPanel.SetActive(false);
            _postProcessVolume.enabled = false;

            foreach (var bonusView in _bonusViews)
            {
                Destroy(bonusView.gameObject);
                bonusView.BonusApplied -= OnBonusApplied;
            }

            _bonusViews.Clear();
        }

        public void MakeBonusVoid(BonusType bonusType)
        {
            var bonusView = _bonusViews.Find(bonusView => bonusView.BonusType == bonusType);
            if (bonusView != null)
            {
                if (bonusView is PlayerStatsBonusView playerStatsBonusView)
                {
                    playerStatsBonusView.ActivateVoid();
                }

                bonusView.BonusApplied -= OnBonusApplied;
            }
        }

        public void IncreasePriceText()
        {
            _tween?.Kill();
            int targetPrice = _changePrice + _amount;
            _tween = DOTween.To(() => _changePrice, x =>
            {
                _changePrice = x;
                _priceText.text = _changePrice.ToString();
            }, targetPrice, _duration);
        }
        
        private void ResetPrice()
        {
            _changePrice = _currentPrice;
            _priceText.text = _changePrice.ToString();
        }
        
        private void OnBonusApplied(BonusType bonusType) => BonusApplied?.Invoke(bonusType);

        private void ReRoll() => ReRolled?.Invoke(_shakePositionAnimation);

        private void Confirm()
        {
            ResetPrice();
            Confirmed?.Invoke();
        } 
    }
}