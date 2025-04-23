using UnityEngine;

namespace _Source.Gameplay.Currency
{
    [CreateAssetMenu(menuName = "ScriptableObject/Config/CurrencyValueConfig", fileName = "CurrencyValueConfig")]
    public class CurrencyValueConfig : ScriptableObject
    {
        [field: SerializeField] public int Gold { get; private set; }
    }
}