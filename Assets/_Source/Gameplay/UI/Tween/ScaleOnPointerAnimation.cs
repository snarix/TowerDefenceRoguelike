using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Source.Gameplay.UI
{
    public class ScaleOnPointerAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float _defaultScale = 1f;
        [SerializeField] private float _increaseScale = 1.1f;
        [SerializeField] private float _scaleDuration = 0.3f;
        
        private Tween _scaleTween;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _scaleTween?.Kill();
            _scaleTween = transform.DOScale(_increaseScale, _scaleDuration);
            
            if (IsSetAsLastSibling())
                _scaleTween.OnStart(() => transform.SetAsLastSibling());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _scaleTween?.Kill();
            _scaleTween = transform.DOScale(_defaultScale, _scaleDuration);
            
            if (IsSetAsFirstSibling())
                _scaleTween.OnComplete(() => transform.SetAsFirstSibling());
        }

        private void OnDestroy()
        {
            _scaleTween?.Kill();
        }
        
        protected virtual bool IsSetAsFirstSibling() => false;
        protected virtual bool IsSetAsLastSibling() => false;
    }
}