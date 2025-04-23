using UnityEngine;

namespace _Source.Gameplay.AbilitySystem.Data
{
    [CreateAssetMenu(menuName = "ScriptableObject/AbilityData/Count", fileName = "CountData")]
    public class CountData : AbilityData
    {
        [SerializeField] private int _count;
        [SerializeField] private float _radius;

        public int Count => _count;
        public float Radius => _radius;
    }
}