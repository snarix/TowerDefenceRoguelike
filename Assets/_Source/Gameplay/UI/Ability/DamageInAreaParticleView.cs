using _Source.Gameplay.AbilitySystem.Abilities;
using UnityEngine;

namespace _Source.Gameplay.UI.Ability
{
    public class DamageInAreaParticleView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _boomFxPrefab;
        private DamageInArea _damageInArea;
        
        public void Initialize(DamageInArea damageInArea)
        {
            _damageInArea = damageInArea;
            _damageInArea.SpawnedExplosion += OnSpawnedExplosion;
        }

        private void OnDestroy()
        {
            _damageInArea.SpawnedExplosion -= OnSpawnedExplosion;
        }

        private void OnSpawnedExplosion(Vector3 position)
        {
            var boomFx = Instantiate(_boomFxPrefab, position, Quaternion.identity);
            boomFx.Play();
        }
    }
}