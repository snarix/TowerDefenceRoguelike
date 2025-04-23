using System.Collections.Generic;
using TowerDefenceRoguelike.Gameplay.Base.Abstractions;
using TowerDefenceRoguelike.Gameplay.Player.Abstractions;
using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Player.EnemyFinder
{
    public class EnemyFinder : IEnemyFinder
    {
        private LayerMask _enemyLayer; 

        public EnemyFinder(LayerMask enemyLayer)    
        {
            _enemyLayer = enemyLayer;
        }

        public IDamageable FindNearestEnemy(Vector3 position, float radius)
        {
            Collider[] enemiesInRange = Physics.OverlapSphere(position, radius, _enemyLayer);
            IDamageable nearestEnemy = null;
            float closeDistance = Mathf.Infinity;

            foreach (Collider enemyCollider in enemiesInRange)
            {
                var distanceToEnemy = Vector3.Distance(position, enemyCollider.transform.position);
                
                if (distanceToEnemy < closeDistance)
                {
                    closeDistance = distanceToEnemy;
                    nearestEnemy = enemyCollider.GetComponent<IDamageable>();
                }
            }

            return nearestEnemy;
        }

        /*public IDamageable FindNearestEnemy(Vector3 position, float radius)
        {
            var enemies = FindAllEnemies(position, radius);
            if (enemies.Count > 0)
                return enemies[0];
            else
                return null;
        }*/

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