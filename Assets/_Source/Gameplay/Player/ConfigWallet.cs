using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Player
{
    [CreateAssetMenu(menuName = "ScriptableObject/ConfigWallet", fileName = "ConfigWallet")]
    public class ConfigWallet : ScriptableObject
    {
        [SerializeField] private int _dollar;
        [SerializeField] private int _gold;

        public int Dollar => _dollar;

        public int Gold => _gold;
    }
}