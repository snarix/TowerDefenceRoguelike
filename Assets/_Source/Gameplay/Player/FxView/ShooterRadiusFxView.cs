using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Player
{
    public class ShooterRadiusFxView : MonoBehaviour
    {
        [SerializeField] private Shooter _shooter;
        [SerializeField] private ParticleSystem _radiusCirclePrefab;
        [SerializeField] private Vector3 _radiusCircleOffset = new Vector3(0f, -2.3f, 0f);
        [SerializeField] private Vector3 _rotation = new Vector3(90f, 0f, 0f);

        private ParticleSystem _radiusRing;
        private PlayerStats _playerStats;

        public void Initialize(PlayerStats playerStats)
        {
            _playerStats = playerStats;
            _radiusRing = Instantiate(_radiusCirclePrefab, transform.position + _radiusCircleOffset, Quaternion.Euler(_rotation));
            _radiusRing.Play();
            
            UpdateRing();
            _playerStats.Radius.OnValueChanged += RadiusOnValueChanged;
        }
        
        private void OnDestroy()
        {
            _playerStats.Radius.OnValueChanged -= RadiusOnValueChanged;
        }
        
        private void RadiusOnValueChanged(float radius)
        {
            UpdateRing();
        }

        private void UpdateRing()
        {
            var shape = _radiusRing.shape;
            shape.radius = _playerStats.Radius.Value;

            _radiusRing.Clear();
            _radiusRing.Play();
        }
        
        private void OnDrawGizmos()
        {
            if(_playerStats == null) return;
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _playerStats.Radius.Value);
        }
    }
}