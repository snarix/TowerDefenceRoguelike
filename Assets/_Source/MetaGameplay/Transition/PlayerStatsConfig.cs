using System;
using TowerDefenceRoguelike.Gameplay.Player;
using UnityEngine;

namespace _Source.MetaGameplay.Transition
{
    [CreateAssetMenu(menuName = "ScriptableObject/Config/PlayerStatsConfig", fileName = "PlayerStatsConfig")]
    [Serializable]
    public class PlayerStatsConfig : ScriptableObject
    {
        [field: SerializeField] public int Damage { get; set; }
        [field: SerializeField] public float Cooldown { get; set; }
        [field: SerializeField] public float Radius { get; set; }
        [field: SerializeField] public int MaxHealth { get; set; }
        [field: SerializeField] public float HealthRegeneration { get; set; }
        [field: SerializeField] public float DollarForWaveMultiplier { get; set; }
        [field: SerializeField] public float GoldForWaveMultiplier { get; set; }
        [field: SerializeField] public float DollarForKillMultiplier { get; set; }
        [field: SerializeField] public float GoldForKillMultiplier { get; set; }

        public PlayerStats GetPlayerStats()
        {
            return new PlayerStats(
                damage: Damage, 
                cooldown: Cooldown,
                radius: Radius,
                maxHealth: MaxHealth,
                healthRegeneration: HealthRegeneration,
                dollarForWaveMultiplier: DollarForWaveMultiplier,
                goldForWaveMultiplier: GoldForWaveMultiplier,
                dollarForKillingMultiplier: DollarForKillMultiplier,
                goldForKillingMultiplier: GoldForKillMultiplier);
        }
    }
}