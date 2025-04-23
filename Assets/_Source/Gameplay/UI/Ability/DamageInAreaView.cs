using _Source.Gameplay.AbilitySystem;
using UnityEngine;

namespace _Source.Gameplay.UI.Ability
{
    public class DamageInAreaView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _zoneRadiusPrefab;
        private ZoneChooser _zoneChooser;
        private Vector3 _currentWorldPosition;
        private bool _isActive;

        public void Initialize(ZoneChooser zoneChooser) => _zoneChooser = zoneChooser;
        
        private void Update()
        {
            if (!_isActive) return;
            
            _currentWorldPosition = _zoneChooser.GetCurrentWorldPosition();
            _zoneRadiusPrefab.transform.position = _currentWorldPosition;
        }

        public void Show(float zoneRadius, Vector3 worldPosition)
        {
            _currentWorldPosition = worldPosition;
            _zoneRadiusPrefab.gameObject.SetActive(true);
            _isActive = true;
            
            _zoneRadiusPrefab.transform.localScale = new Vector2(zoneRadius, zoneRadius);
        }

        public void Hide()
        {
            _zoneRadiusPrefab.gameObject.SetActive(false);
            _isActive = false;
        }
    }
}