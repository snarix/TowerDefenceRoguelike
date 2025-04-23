using System;
using UnityEngine;

namespace _Source.Gameplay.Currency
{
    [Serializable]
    public class Reward
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private int _dollar;
        [SerializeField] private int _gold;

        public EnemyType EnemyType => _enemyType;
        
        public int Dollar => _dollar;

        public int Gold => _gold;
    }
}