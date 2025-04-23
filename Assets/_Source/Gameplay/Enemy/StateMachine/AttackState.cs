namespace TowerDefenceRoguelike.Gameplay.Enemy.StateMachine
{
    public class AttackState : BaseState
    {
        public AttackState(Enemy enemy, IEnemyStateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void Enter()
        {
            _enemy.AnimationHandler.Attack += OnAttack;
            _enemy.Animator.SetAttackSpeed(_enemy.Attacker.PauseBetweenAttack);
            _enemy.Animator.TriggerAttack();
        }

        public override void Update()
        {
            if (_enemy.Attacker.IsTargetInRange(_enemy.Target) == false)
            {
                _stateMachine.SwitchState<RunState>();
                return;
            }
            
            if (_enemy.Attacker.CanAttack())
            {
                _enemy.Animator.TriggerAttack();
            }
        }

        public override void Exit()
        {
            _enemy.AnimationHandler.Attack -= OnAttack;
        }
        
        private void OnAttack()
        {
            if (_enemy.Attacker.CanAttack()) 
            {
                _enemy.Attacker.Attack(_enemy.AttackTarget);
            }
        }
    }
}