using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _radius;
    [SerializeField] private float _rotationSpeed;
    private Transform _target;
        
    public bool IsLookingAt { get; private set; }

    private void Update()
    {
        TryFindTarget();
        if (_target != null)
        {
            SmoothLookAt(_target.position);
            IsLookingAt = true;
        }
        else
        {
            IsLookingAt = false;
        }
    }
        
    public HPView GetTargetHPView()
    {
        if (_target != null)
        {
            return _target.GetComponent<HPView>();
        }
        return null;
    }
        
    private void TryFindTarget()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, _radius, _enemyLayer);
        Transform closeEnemy = null;
        float closeDistance = Mathf.Infinity;

        foreach (Collider enemyCollider in enemiesInRange)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemyCollider.transform.position);
            if (distanceToEnemy < closeDistance)
            {
                closeDistance = distanceToEnemy;
                closeEnemy = enemyCollider.transform;
            }
        }

        _target = closeEnemy;
    }

    private void SmoothLookAt(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
    }
        
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}