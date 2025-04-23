using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Base.Abstractions
{
    public interface IDamageable
    {
        Vector3 Position { get; }
        void TakeDamage(int damage);
    }
}