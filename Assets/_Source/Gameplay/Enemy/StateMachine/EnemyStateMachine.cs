using System.Collections.Generic;

namespace TowerDefenceRoguelike.Gameplay.Enemy.StateMachine
{
    public class EnemyStateMachine : IEnemyStateMachine
    {
        private IEnemyState _currentState;
        private List<IEnemyState> _states;

        public EnemyStateMachine(Enemy enemy)
        {
            _states = new List<IEnemyState>
            {
                new IdleState(enemy, this),
                new RunState(enemy, this),
                new AttackState(enemy, this),
                new DeathState(enemy, this)
            };
            
            SwitchState<IdleState>();
        }

        public void Update()
        {
            _currentState?.Update();
        }
        
        public void SwitchState<State>() where State : IEnemyState
        {
            _currentState?.Exit();

            _currentState = _states.Find(s => s.GetType() == typeof(State));
            
            _currentState?.Enter();
        }
    }
}