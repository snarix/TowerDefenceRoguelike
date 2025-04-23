using System.Collections.Generic;
using TowerDefenceRoguelike.Gameplay.Base.Abstractions;
using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Player.Abstractions
{
    public interface IEnemyFinder : IEnemiesFinder
    {
        IDamageable FindNearestEnemy(Vector3 position, float radius);
    }

    public interface IEnemiesFinder
    {
        List<IDamageable> FindAllEnemies(Vector3 position, float radius);
    }
}