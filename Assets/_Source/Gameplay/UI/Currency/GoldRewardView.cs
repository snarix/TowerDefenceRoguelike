using TMPro;
using UnityEngine;

namespace _Source.Gameplay.UI
{
    public class GoldRewardView : RewardView
    {
        [SerializeField] private TextMeshProUGUI _goldText;

        protected override void RewardUpdated(float dollar, float gold)
        {
            _goldText.text = Mathf.RoundToInt(gold).ToString();
        }
    }
}