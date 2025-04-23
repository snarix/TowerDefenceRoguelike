using System.Collections.Generic;
using TowerDefenceRoguelike.Gameplay.Base.Abstractions;
using TowerDefenceRoguelike.Gameplay.Player.Abstractions;
using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Player.EnemyFinder
{
    public class EnemyFinderInArea : IEnemiesFinder
    {
        private LayerMask _enemyLayer;

        public EnemyFinderInArea(LayerMask enemyLayer)
        {
            _enemyLayer = enemyLayer;
        }

        public List<IDamageable> FindAllEnemies(Vector3 position, float radius)
        {
            Collider[] enemiesInRange = Physics.OverlapSphere(position, radius, _enemyLayer);
            List<IDamageable> enemies = new List<IDamageable>();

            foreach (Collider enemyCollider in enemiesInRange)
            {
                IDamageable enemy = enemyCollider.GetComponent<IDamageable>();
                if (enemy != null)
                {
                    enemies.Add(enemy);
                }
            }

            return enemies;
        }
    }
}