using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Source.Gameplay.Base
{
    public class InputSystem : IDisposable
    {
        private GameInput _gameInput;
        private InputAction _selectAction;
        private InputAction _cancelAction;
        
        public event Action<Vector2> Selected;
        public event Action Canceled;

        public InputSystem()
        {
            _gameInput = new GameInput();
            _selectAction = _gameInput.Gameplay.Select;
            _cancelAction = _gameInput.Gameplay.Cancel;
            
            _selectAction.performed += SelectInput;
            _cancelAction.performed += CancelInput;
        }
        
        public void Dispose()
        {
            _selectAction.performed -= SelectInput;
            _cancelAction.performed -= CancelInput;
            Disable();
        }
        
        public void Enable()
        {
            _selectAction.Enable();
            _cancelAction.Enable();
        }

        public void Disable()
        {
            _selectAction.Disable();
            _cancelAction.Disable();
        }
        
        public Vector2 GetCurrentScreenPosition()
        {
            if (Mouse.current != null)
                return Mouse.current.position.ReadValue();
            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.tap.isPressed)
                return Touchscreen.current.primaryTouch.position.ReadValue();
            return Vector2.zero;
        }
        
        private void SelectInput(InputAction.CallbackContext obj)
        {
            Vector2 screenPosition = GetCurrentScreenPosition();
            Selected?.Invoke(screenPosition);
        }
        
        private void CancelInput(InputAction.CallbackContext obj) => Canceled?.Invoke();
    }
}