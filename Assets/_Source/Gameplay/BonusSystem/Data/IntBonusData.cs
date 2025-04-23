using UnityEngine;

namespace _Source.Gameplay.BonusSystem.Data
{
    [CreateAssetMenu(menuName = "ScriptableObject/BonusData/Int", fileName = "IntBonusData")]
    public class IntBonusData : BonusData
    {
        [SerializeField] private int _bonusValue;
        public int BonusValue => _bonusValue;
        
        /*private void OnValidate()
        {
            if (BonusDescriptionFormat.Contains("{0}") == false)
            {
                Debug.LogError("BonusDescriptionFormat is invalid");
            }
        }*/
    }
}