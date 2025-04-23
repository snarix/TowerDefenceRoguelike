namespace TowerDefenceRoguelike.Gameplay.Enemy.StateMachine
{
    public abstract class BaseState : IEnemyState
    {
        protected readonly Enemy _enemy;
        protected readonly IEnemyStateMachine _stateMachine;

        protected BaseState(Enemy enemy, IEnemyStateMachine stateMachine)
        {
            _enemy = enemy;
            _stateMachine = stateMachine;
        }

        public abstract void Enter();

        public abstract void Update();

        public abstract void Exit();
    }
    
    public interface IEnemyState
    {
        void Enter();
        void Update();
        void Exit();
    }
}