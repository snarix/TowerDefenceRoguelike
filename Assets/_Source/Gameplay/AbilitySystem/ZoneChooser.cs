using System;
using _Source.Gameplay.Base;
using UnityEngine;

namespace _Source.Gameplay.AbilitySystem
{
    public class ZoneChooser : IDisposable
    {
        private Camera _camera;
        private InputSystem _inputSystem;

        private Action<Vector3> _zoneSelected;
        public event Action ZoneCanceled;
        private bool _isChoosing;

        public ZoneChooser(Camera camera, InputSystem inputSystem)
        {
            _camera = camera;
            _inputSystem = inputSystem;
            
            _inputSystem.Selected += OnSelected;
            _inputSystem.Canceled += OnCanceled;
        }
        
        public void Dispose()
        {
            _inputSystem.Selected -= OnSelected;
            _inputSystem.Canceled -= OnCanceled;
        }
        
        public void StartChoosing(Action<Vector3> selectedZone)
        {
            if (_isChoosing) return;
            
            _isChoosing = true;
            _zoneSelected = selectedZone;
            _inputSystem.Enable();
        }
        
        public Vector3 GetCurrentWorldPosition()
        {
            Vector2 screenPosition = _inputSystem.GetCurrentScreenPosition();
            return GetWorldPosition(screenPosition);
        }
        
        private void OnSelected(Vector2 screenPosition)
        {
            if (!_isChoosing) return;
            
            Vector3 worldPosition = GetWorldPosition(screenPosition);
            _zoneSelected?.Invoke(worldPosition);
            _isChoosing = false;
        }
        
        private void OnCanceled()
        {
            if (!_isChoosing) return;
            
            ZoneCanceled?.Invoke();
            StopChoosing();
        }

        private void StopChoosing()
        {
            _isChoosing = false;
            _inputSystem.Disable();
        }

        private Vector3 GetWorldPosition(Vector2 screenPosition)
        {
            Ray ray = _camera.ScreenPointToRay(screenPosition);

            Plane plane = new Plane(Vector3.up, Vector3.zero);

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 position = ray.GetPoint(distance);
                position.y += 0.1f;
                return position;
            }

            return Vector3.zero;
        }
    }
}