using System;
using UnityEngine;

namespace _Source.Gameplay.Currency
{
    [Serializable]
    public class Currency
    {
        [SerializeField] private float _value;

        public event Action<float> BalanceChange;
        
        public float Value => _value;
        
        public Currency(float value)
        {
            _value = value;
        }

        public void Add(float amount)
        {
            if(amount < 0)
                throw new ArgumentException("Amount must be greater than 0");
            
            _value += amount;
            BalanceChange?.Invoke(_value);
        }
        
        public void Spend(int amount)
        {
            if(_value < amount)
                throw new ArgumentException("Cannot spend value");
            
            _value -= amount;
            BalanceChange?.Invoke(_value);
        }

        public bool IsEnough(int amount)
        {
            return _value >= amount;
        }
    }
}