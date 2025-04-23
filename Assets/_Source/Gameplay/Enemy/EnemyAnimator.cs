using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Enemy
{
    public class EnemyAnimator : MonoBehaviour
    {
        private const string IS_RUNNING = "isRunning";
        private const string ATTACK = "Attack";
        private const string IS_DEATH = "isDeath";
        private const string ATTACK_MULTIPLIER = "AttackSpeed";
        private const float PAUSE_BETWEEN_ATTACK_TO_ATTACK_SPEED = 1;

        [SerializeField] private Animator _animator;
        
        public void SetMove(bool isMoving)
        {
            _animator.SetBool(IS_RUNNING, isMoving);
        }

        public void TriggerAttack()
        {
            _animator.SetTrigger(ATTACK);
        }
        
        public void SetAttackSpeed(float pauseBetweenAttack)
        {
            _animator.SetFloat(ATTACK_MULTIPLIER, PAUSE_BETWEEN_ATTACK_TO_ATTACK_SPEED / pauseBetweenAttack);
        }

        public void SetDeath()
        {
            _animator.SetBool(IS_DEATH, true);
        }
    }
}
