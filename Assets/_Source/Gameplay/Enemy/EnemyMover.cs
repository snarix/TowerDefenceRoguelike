using UnityEngine;
using UnityEngine.AI;

namespace TowerDefenceRoguelike.Gameplay.Enemy
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private float _speed = 5f;

        public void Initialize()
        {
            _navMeshAgent.speed = _speed;
        }

        public void Move(Vector3 targetPosition)
        {
            _navMeshAgent.SetDestination(targetPosition);
        }

        public void Stop()
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.ResetPath();
        }
    }
}