using DG.Tweening;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

namespace _Source.Gameplay.UI
{
    public class FadeAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject _fadePanel;
        [SerializeField] private GameObject _vignette;
        
        [SerializeField] private float _durationStart = 0.5f;
        [SerializeField] private float _holdDuration = 0.1f;
        [SerializeField] private float _durationEnd = 0.5f;

        private Sequence _sequence;

        private void OnDestroy()
        {
            _sequence?.Kill();
        }

        public void CreateVignette()
        {
            var vignette =  Instantiate(_vignette, _fadePanel.transform);
            var vignetteCanvasGroup = vignette.GetComponent<CanvasGroup>();

            _sequence = DOTween.Sequence();
            _sequence.
                Append(vignetteCanvasGroup.DOFade(1f, _durationStart).From(0)).
                AppendInterval(_holdDuration).
                Append(vignetteCanvasGroup.DOFade(0f, _durationEnd).From(1)).
                OnComplete(() => Destroy(vignette));
        }
    }
}