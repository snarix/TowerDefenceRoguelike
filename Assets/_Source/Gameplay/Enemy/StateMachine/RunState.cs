namespace TowerDefenceRoguelike.Gameplay.Enemy.StateMachine
{
    public class RunState : BaseState
    {
        public RunState(Enemy enemy, IEnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void Enter()
        {
            _enemy.Animator.SetMove(true);
        }

        public override void Update()
        {
            _enemy.Mover.Move(_enemy.Target);
            
            if (_enemy.Attacker.IsTargetInRange(_enemy.Target))
            {
                _stateMachine.SwitchState<AttackState>();
            }
        }

        public override void Exit()
        {
            _enemy.Animator.SetMove(false);
            _enemy.Mover.Stop();
        }
    }
}