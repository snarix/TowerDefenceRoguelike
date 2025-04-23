using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Enemy.Factory
{
    public interface IEnemyFactory
    {
        Enemy CreateEnemy(Vector3 spawnPosition, Enemy enemyPrefab);
    }
}