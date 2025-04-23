using _Source.Gameplay.AbilitySystem.Abstractions;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.Gameplay.AbilitySystem.Data
{
    public abstract class AbilityData : ScriptableObject
    {
        [SerializeField] private AbilityType _abilityType;
        
        [SerializeField] private Image _abilityImage;
        
        [SerializeField] private float value;
        
        public AbilityType AbilityType => _abilityType;

        public Image AbilityImage => _abilityImage;

        public float Value => value;
    }
}