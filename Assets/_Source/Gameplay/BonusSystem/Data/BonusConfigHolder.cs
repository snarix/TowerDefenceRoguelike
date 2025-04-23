using System.Collections.Generic;
using UnityEngine;

namespace _Source.Gameplay.BonusSystem.Data
{
    [CreateAssetMenu(menuName = "ScriptableObject/ConfigHolder/Bonus", fileName = "BonusConfigHolder")]
    public class BonusConfigHolder : ScriptableObject
    {
        [field: SerializeField] public List<BonusData> Bonuses { get; set; }

        public T GetBonusData<T>(BonusType bonusType) where T : BonusData
        {
            var bonusData = Bonuses.Find(bonus => bonus.BonusType == bonusType);
            if (bonusData != null && bonusData is T)
                return (T)bonusData;

            throw new System.Exception($"No bonus found for type {typeof(T)} of {bonusType}");
        }
    }
}