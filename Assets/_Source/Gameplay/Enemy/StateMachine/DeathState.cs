using _Source.Gameplay.UI.HealthView;
using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Enemy.StateMachine
{
    public class DeathState : BaseState
    {
        public DeathState(Enemy enemy, IEnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void Enter()
        {
            _enemy.Animator.SetDeath();
            _enemy.Animator.SetMove(false);
            
            _enemy.Collider.enabled = false;
            
            if (_enemy.HealthView is ImageHealthView imageHealthView)
                imageHealthView.FadeImage();
            
            Object.Destroy(_enemy.gameObject, _enemy.DestructionDelay);
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}