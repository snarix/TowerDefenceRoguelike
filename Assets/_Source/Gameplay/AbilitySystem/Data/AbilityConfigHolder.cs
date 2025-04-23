using System;
using System.Collections.Generic;
using _Source.Gameplay.AbilitySystem.Abstractions;
using UnityEngine;

namespace _Source.Gameplay.AbilitySystem.Data
{
    [CreateAssetMenu(menuName = "ScriptableObject/ConfigHolder/Ability", fileName = "AbilityConfigHolder")]
    public class AbilityConfigHolder : ScriptableObject
    {
        [field: SerializeField] public List<AbilityData> Abilities { get; private set; }

        public T GetAbilityData<T>(AbilityType abilityType) where T : AbilityData
        {
            var ability = Abilities.Find(x => x.AbilityType == abilityType);
            if (ability != null && ability is T)
                return (T)ability;
            
            throw new Exception($"No bonus found for type {typeof(T)} of {abilityType}");
        }
    }
}