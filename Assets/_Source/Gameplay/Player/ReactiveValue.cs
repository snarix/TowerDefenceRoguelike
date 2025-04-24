using System;
using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Player
{
    [Serializable]
    public class ReactiveValue<T>
    {
        [SerializeField] private T _value;

        public ReactiveValue(T value)
        {
            _value = value;
        }

        public event Action<T> OnValueChanged;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }
    }
}