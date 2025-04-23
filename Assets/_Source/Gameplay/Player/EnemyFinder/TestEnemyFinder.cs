using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefenceRoguelike.Gameplay.Base.Abstractions;
using TowerDefenceRoguelike.Gameplay.Player.Abstractions;
using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Player.EnemyFinder
{
    public class TestEnemyFinder : IEnemyFinder
    {
        private IEnumerable<IDamageable> _damageables;

        public TestEnemyFinder(IEnumerable<IDamageable> damageables)
        {
            UpdateDamageables(damageables);
        }

        public bool HasEnemies() => _damageables.Any();

        public IDamageable FindNearestEnemy(Vector3 position, float radius)
        {
            if (HasEnemies() == false)
                throw new Exception("No enemies found");
            
            IDamageable nearestEnemy = null;
            float closeDistance = Mathf.Infinity;

            foreach (IDamageable damageable in _damageables)
            {
                var distanceToEnemy = Vector3.Distance(position, damageable.Position);

                if (distanceToEnemy < closeDistance)
                {
                    closeDistance = distanceToEnemy;
                    nearestEnemy = damageable;
                }
            }

            return nearestEnemy;
        }

        public List<IDamageable> FindAllEnemies(Vector3 position, float radius)
        {
            throw new NotImplementedException();
        }

        public void UpdateDamageables(IEnumerable<IDamageable> damageables)
        {
            _damageables = damageables;
        }
    }
}