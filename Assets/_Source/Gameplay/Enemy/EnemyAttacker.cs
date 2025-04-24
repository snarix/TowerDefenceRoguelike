using System;
using TowerDefenceRoguelike.Gameplay.Base;
using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Enemy
{
    public class EnemyAttacker : MonoBehaviour
    {
        [SerializeField] private float _attackRange = 1.5f;
        [SerializeField] private float _pauseBetweenAttack = 2f;
        private float _damage;
        
        private float _nextAttackTime = 0f;

        public float PauseBetweenAttack => _pauseBetweenAttack;

        public void Initialize(EnemyStatsTemp stats)
        {
            _damage = stats.Damage;
        }

        public bool IsTargetInRange(Vector3 target) => Vector3.Distance(transform.position, target) <= _attackRange;

        public bool CanAttack() => Time.time > _nextAttackTime;

        public void Attack(Health target)
        {
            if (CanAttack() == false)
                throw new Exception("Can not attack");
            
            target.TakeDamage(Mathf.RoundToInt(_damage));
            
            _nextAttackTime = Time.time + _pauseBetweenAttack;
        }
    }
}