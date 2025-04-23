using System;
using UnityEngine;

namespace _Source.Gameplay.Currency
{
    public class GameplayWallet
    {
        private Currency _dollar;
        private Currency _gold;
        
        public GameplayWallet(Currency dollar, Currency gold)
        {
            _dollar = dollar;
            _gold = gold;
        }
        
        public Currency Dollar => _dollar;

        public Currency Gold => _gold;
    }
    
    [Serializable]
    public class MetaWallet
    {
        [SerializeField] private Currency _gold;
        
        public MetaWallet(Currency gold)
        {
            _gold = gold;
        }

        public Currency Gold => _gold;
    }
}