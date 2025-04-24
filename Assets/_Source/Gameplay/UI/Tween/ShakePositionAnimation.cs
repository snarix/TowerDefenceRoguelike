using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.Gameplay.UI
{
    public class ShakePositionAnimation : MonoBehaviour
    {
        [SerializeField] private Button _bonusButton; 
        [SerializeField] private float _lockAnimationDuration = 0.4f;
        [SerializeField] private float _lockAnimationStrenght = 2f;
        
        private bool _isLocked;
        
        public bool Lock() => _isLocked;

        public bool Unlock() => _isLocked = false;
        
        public void OnButtonShakeClick()
        {
            if (_isLocked)
            {
                transform.DOShakePosition(_lockAnimationDuration, _lockAnimationStrenght);
            }
        }
    }
}