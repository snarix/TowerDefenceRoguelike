using UnityEngine;

namespace _Source.Gameplay.AbilitySystem.Data
{
    [CreateAssetMenu(menuName = "ScriptableObject/AbilityData/Cooldown", fileName = "CooldownData")]
    public class CooldownData : AbilityData
    {
        [SerializeField] private float _cooldown;
        [SerializeField] private float _duration;
        public float Cooldown => _cooldown;
        public float Duration => _duration;
    }
}