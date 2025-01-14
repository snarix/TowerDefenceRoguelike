using System.Collections;

using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private LookAtTarget _lookAtTarget;
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown = 1f;
    private IAttackable _attacker;
    private bool _canAttack = true;

    private void Awake()
    {
        _attacker = new AttackView(_damage);
    }

    private void Update()
    {
        if (_lookAtTarget.IsLookingAt && _canAttack)
        {
            HPView currentTarget = _lookAtTarget.GetTargetHPView();
            StartCoroutine(Attack(currentTarget));
        }
    }

    private IEnumerator Attack(HPView target)
    {
        _canAttack = false;
        
        _attacker.Attack(target);
        
        yield return new WaitForSeconds(_cooldown);
        
        _canAttack = true;
    }
}