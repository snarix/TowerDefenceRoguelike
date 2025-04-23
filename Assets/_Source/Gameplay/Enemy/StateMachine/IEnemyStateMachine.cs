namespace TowerDefenceRoguelike.Gameplay.Enemy.StateMachine
{
    public interface IEnemyStateMachine
    {
        void SwitchState<State>() where State : IEnemyState;
    }
}