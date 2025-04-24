using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace _Source.Gameplay.UI
{
    public class ExitPanelView : MonoBehaviour
    {
        [SerializeField] private GameObject _areYouSurePanel;
        [SerializeField] private Button _yesButton;
        [SerializeField] private Button _noButton;
        
        [SerializeField] private GameObject _screenDeathPanel;
        [SerializeField] private TextMeshProUGUI _goldText;
        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _toBaseButton;
        
        [SerializeField] private Image _background;

        public GameObject AreYouSurePanel => _areYouSurePanel;

        public GameObject ScreenDeathPanel => _screenDeathPanel;

        public event Action YesClicked;
        public event Action NoClicked;
        public event Action RetryClicked;
        public event Action ToBaseClicked;

        public void Initialize(int gold)
        {
            _goldText.text = gold.ToString();
        }
        
        private void OnEnable()
        {
            _yesButton.onClick.AddListener(YesClick);
            _noButton.onClick.AddListener(NoClick);
            _retryButton.onClick.AddListener(RetryClick);
            _toBaseButton.onClick.AddListener(ToBaseClick);
        }

        private void OnDisable()
        {
            _yesButton.onClick.RemoveListener(YesClick);
            _noButton.onClick.RemoveListener(NoClick);
            _retryButton.onClick.RemoveListener(RetryClick);
            _toBaseButton.onClick.RemoveListener(ToBaseClick);
        }
        
        private void YesClick() => YesClicked?.Invoke();
        
        private void NoClick() => NoClicked?.Invoke();

        private void RetryClick()
        {
            YG2.InterstitialAdvShow();
            RetryClicked?.Invoke();
        }

        private void ToBaseClick()
        {
            YG2.InterstitialAdvShow();
            ToBaseClicked?.Invoke();
        } 
    }
}