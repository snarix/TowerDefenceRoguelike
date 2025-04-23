using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.Gameplay.UI
{
    public class TimePanelView : MonoBehaviour
    {
        [SerializeField] private Button _plusButton;
        [SerializeField] private Button _minusButton;
        
        [SerializeField] private TextMeshProUGUI _timeText;
        
        private float _defaultTime = 1f;
        private float _fastTime = 3f;

        public void OnEnable()
        {
            _plusButton.onClick.AddListener(PlusClick);
            _minusButton.onClick.AddListener(MinusClick);
            SetTimeScale(_defaultTime);
        }

        private void OnDisable()
        {
            _plusButton.onClick.RemoveListener(PlusClick);
            _minusButton.onClick.RemoveListener(MinusClick);
        }

        private void PlusClick()
        {
            SetTimeScale(_fastTime);
            UpdateButtonsInteractable();
        }
        
        private void MinusClick()
        {
            SetTimeScale(_defaultTime);
            UpdateButtonsInteractable();
        }

        private void SetTimeScale(float timeScale)
        {
            Time.timeScale = timeScale;
            _timeText.text = $"X{timeScale}";
        }
        
        private void UpdateButtonsInteractable()
        {
            _plusButton.interactable = !Mathf.Approximately(Time.timeScale, _fastTime);
            _minusButton.interactable = !Mathf.Approximately(Time.timeScale, _defaultTime);
        }
    }
}