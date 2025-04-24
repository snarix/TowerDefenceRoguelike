using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Player
{
    public class ShooterShootFxView : MonoBehaviour
    {
        [SerializeField] private Shooter _shooter;
        [SerializeField] private ParticleSystem _shootFx;
        [SerializeField] private Transform _shootPosition;
        
        private void OnEnable()
        {
            _shooter.ShootSpawned += OnShootSpawned;
        }

        private void OnDestroy()
        {
            _shooter.ShootSpawned -= OnShootSpawned;
        }

        private void OnShootSpawned()
        {
            var shoot = Instantiate(_shootFx, _shootPosition.position, _shootPosition.rotation);
            shoot.Play();
        }
    }
}