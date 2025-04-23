using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.Gameplay.UI.HealthView
{
    public class ImageHealthView : HealthView
    {
        [SerializeField] private Image _healthBarFilling;
        [SerializeField] private Image _healthBar;
        
        [SerializeField] private float _endValue = 0f;
        [SerializeField] private float _duration = 0.5f;
        
        protected override void OnHealthUpdated(float currentHealthNormalized, float maxHealth)
        {
            _healthBarFilling.fillAmount = currentHealthNormalized;
        }
        
        public void FadeImage()
        {
            _healthBar.DOFade(_endValue, _duration);
        }
    }
}