using TowerDefenceRoguelike.Gameplay.Player;
using UnityEngine;

namespace _Source.Gameplay.UI
{
    public abstract class RewardView : MonoBehaviour
    {
        private RewardGiver _rewardGiver;

        public void Initialize(RewardGiver rewardGiver)
        {
            _rewardGiver = rewardGiver;
            _rewardGiver.RewardedForWave += OnReward;
        }

        private void OnDestroy()
        {
            _rewardGiver.RewardedForWave -= OnReward;
        }

        private void OnReward(float dollar, float gold)
        {
            RewardUpdated(dollar, gold);
        }

        protected abstract void RewardUpdated(float dollar, float gold);
    }
}