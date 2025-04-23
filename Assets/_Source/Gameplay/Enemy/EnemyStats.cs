using System;
using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Enemy
{
    [Serializable]
    public class EnemyStats 
    {
        [SerializeField] private float _damage;
        [SerializeField] private int _maxHealth;

        [SerializeField] private float _amountDamage;
        [SerializeField] private int _amountHealth;
        [SerializeField] private int _inFirstWave;

        public float Damage => _damage;

        public int MaxHealth => _maxHealth;

        public float AmountDamage => _amountDamage;

        public int AmountHealth => _amountHealth;
        
        public int InFirstWave => _inFirstWave;
    }

    public class EnemyStatsTemp
    {
        public float Damage { get; }
        public int MaxHealth { get; }

        public EnemyStatsTemp(float damage, int maxHealth)
        {
            Damage = damage;
            MaxHealth = maxHealth;
        }
    }
}