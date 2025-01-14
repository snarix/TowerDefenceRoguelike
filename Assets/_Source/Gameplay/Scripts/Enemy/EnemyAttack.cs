using UnityEngine;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private HPView _hpView;
        [SerializeField] private int _damage;
        private IAttackable _attacker;

        private void Awake()
        {
            _attacker = new AttackView(_damage);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _attacker.Attack(_hpView);
            }
        }
    }
}