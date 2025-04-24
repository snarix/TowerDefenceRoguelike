using System.Collections;
using _Source.Gameplay.UI;
using TowerDefenceRoguelike.Gameplay.Base;
using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Player
{
    public class PlayerExplosionFxView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _explosionFX;
        [SerializeField] private MeshRenderer _carVisual;
        [SerializeField] private FadeAnimation _fadeAnimation;
        [SerializeField] private float _explosionRadiusMultiplier = 7f;

        private Player _player;

        public void Initialize(Player player)
        {
            _player = player;
            
            _player.Health.Damaged += OnDamaged;
            _player.Health.Died += OnHealthDied;
        }

        private void OnDestroy()
        {
            _player.Health.Damaged -= OnDamaged;
            _player.Health.Died -= OnHealthDied;
        }
        
        public float GetExplosionRadius()
        {
            ParticleSystem.ShapeModule shapeModule = _explosionFX.shape;
            return shapeModule.radius * _explosionRadiusMultiplier;
        }

        private void OnDamaged(int damage)
        {
            _fadeAnimation.CreateVignette();
        }

        private void OnHealthDied(Health health)
        {
            var boomFx = Instantiate(_explosionFX, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
            boomFx.Play();

            _carVisual.gameObject.SetActive(false);
            _player.Shooter.gameObject.SetActive(false);

            StartCoroutine(WaitForExplosion());
        }

        private IEnumerator WaitForExplosion()
        {
            yield return new WaitForSeconds(_explosionFX.main.duration);
            gameObject.SetActive(false);
        }
    }
}