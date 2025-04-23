using UnityEngine;
using UnityEngine.AI;

namespace TowerDefenceRoguelike.Gameplay.Enemy
{
    public class EnemyMover : MonoBehaviour
    {
        //[SerializeField] private CharacterController _characterController;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private float _speed = 5f;

        private void Start()
        {
            _navMeshAgent.speed = _speed;
        }

        public void Move(Vector3 targetPosition)
        {
            //Vector3 direction = (targetPosition - _characterController.transform.position).normalized;
            //Vector3 velocity = direction * (_speed * Time.deltaTime);
           // if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                _navMeshAgent.SetDestination(targetPosition);
            //_characterController.Move(velocity);
            //_characterController.transform.LookAt(targetPosition);
        }

        public void Stop()
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.ResetPath();
        }
    }
}