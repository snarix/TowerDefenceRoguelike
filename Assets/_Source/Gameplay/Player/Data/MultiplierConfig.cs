using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Player
{
    [CreateAssetMenu(menuName = "ScriptableObject/MultiplierConfig", fileName = "MultiplierConfig")]
    public class MultiplierConfig : ScriptableObject
    {
        [SerializeField] private int _multiplier;

        public int Multiplier => _multiplier;
    }
}