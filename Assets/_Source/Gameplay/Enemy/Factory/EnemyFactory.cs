using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Enemy.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        public Enemy CreateEnemy(Vector3 spawnPosition, Enemy enemyPrefab) => Object.Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}