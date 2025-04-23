using System.Collections.Generic;
using _Source.Gameplay.Currency;
using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Enemy.Factory
{
    [System.Serializable]
    public class SpawnData
    {
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private List<Wave> _waves = new List<Wave>();
        [SerializeField][Min(0)] private float _timeBetweenWaves = 5f;
        
        public List<Transform> SpawnPoints => _spawnPoints;
        public IReadOnlyList<Wave> Waves => _waves;
        public float TimeBetweenWaves => _timeBetweenWaves;
    }
    
    [System.Serializable]
    public class Wave
    {
        [SerializeField] private Reward _reward;
        [SerializeField][Min(1)] private List<Enemy> _enemiesPerWave;
        [SerializeField][Min(0)] private float _timeBetweenSpawn = 1f;

        public Reward Reward => _reward;
        public List<Enemy> EnemiesPerWave => _enemiesPerWave;
        public float TimeBetweenSpawn => _timeBetweenSpawn;
    }
}