using TowerDefenceRoguelike.Gameplay.Base.Abstractions;
using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Player
{
    public class ShooterBloodFxView : MonoBehaviour
    {
        [SerializeField] private Shooter _shooter;
        [SerializeField] private ParticleSystem _bloodFx;
        
        private void OnEnable()
        {
            _shooter.BloodSpawned += OnBloodSpawned;
        }
        
        private void OnDestroy()
        {
            _shooter.BloodSpawned -= OnBloodSpawned;
        }
        
        private void OnBloodSpawned(IDamageable nearestEnemy)
        {
            var blood = Instantiate(_bloodFx, nearestEnemy.Position, Quaternion.identity);
            blood.Play();
        }
    }
}