using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Source.Gameplay.UI
{
    public class KillTextAnimation : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Vector2 _targetPosition;
        [SerializeField] private Vector2 _startPosition;
        
        [SerializeField] private float _duration = 0.3f;
        
        private Sequence _sequence;
        
        private void OnDestroy()
        {
            _sequence?.Kill();
        }
        
        public void Show()
        {
            _sequence = DOTween.Sequence();

            _sequence.
                Append(_text.DOFade(1f, _duration).From(0)).
                Join(_rectTransform.DOAnchorPos(_targetPosition, _duration).From(_startPosition)).
                Append(_text.DOFade(0f, _duration).From(1));
        }
    }
}