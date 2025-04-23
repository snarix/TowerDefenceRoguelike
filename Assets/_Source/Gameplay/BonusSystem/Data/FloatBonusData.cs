using UnityEngine;

namespace _Source.Gameplay.BonusSystem.Data
{
    [CreateAssetMenu(menuName = "ScriptableObject/BonusData/Float", fileName = "FloatBonusData")]
    public class FloatBonusData : BonusData
    {
        [SerializeField] private float _bonusValue;
        public float BonusValue => _bonusValue;

        /*private void OnValidate()
        {
            if (BonusDescriptionFormat.Contains("{0}") == false)
            {
                Debug.LogError("BonusDescriptionFormat is invalid");
            }
        }*/
    }
}