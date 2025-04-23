using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.Gameplay.UI
{
    public class WaveTextAnimation : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _image;
        
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
                Join(_image.DOFade(1f, _duration).From(0));
        }

        public void Hide()
        {
            _sequence = DOTween.Sequence();
            
            _sequence.
                Append(_text.DOFade(0f, _duration).From(1)).
                Join(_image.DOFade(0f, _duration).From(1));
            
        }
    }
}