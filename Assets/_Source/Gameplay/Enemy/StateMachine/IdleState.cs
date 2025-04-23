namespace TowerDefenceRoguelike.Gameplay.Enemy.StateMachine
{
    public class IdleState : BaseState
    {
        public IdleState(Enemy enemy, IEnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
            
        }
        
        public override void Enter()
        {
            _enemy.Animator.SetMove(false);
        }

        public override void Update()
        {
            if (_enemy.AttackTarget != null)
            {
                _stateMachine.SwitchState<RunState>();
            }
        }

        public override void Exit()
        {
            
        }
    }
}