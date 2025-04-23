using System;
using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Enemy
{
    public class EnemyAnimationEventsHandler: MonoBehaviour
    {
        public event Action Attack;
        public event Action DeathFinished;
        
        public void TriggerAttack()
        {
            Attack?.Invoke();
        }

        public void SetDeath()
        {
            DeathFinished?.Invoke();
        }
    }
}