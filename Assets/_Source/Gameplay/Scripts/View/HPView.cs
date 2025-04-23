using TowerDefenceRoguelike.Gameplay.Base.Abstractions;
using UnityEngine;

public class HPView : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health;

    public Vector3 Position { get; }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            _health = 0;
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
