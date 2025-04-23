using System.Collections.Generic;
using UnityEngine;

namespace _Source.Gameplay.Currency
{
    [CreateAssetMenu(menuName = "ScriptableObject/RewardConfig", fileName = "RewardConfig")]
    public class RewardConfig : ScriptableObject
    {
        [SerializeField] private List<Reward> _rewards;

        public Reward GetReward(EnemyType type)
        {
            var reward = _rewards.Find(x => x.EnemyType == type);
            
            return reward;
        }
    }
}