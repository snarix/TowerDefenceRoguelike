using TMPro;
using UnityEngine;

namespace _Source.Gameplay.UI
{
    public class DollarRewardView : RewardView
    {
        [SerializeField] private TextMeshProUGUI _dollarWaveText;

        protected override void RewardUpdated(float dollar, float gold)
        {
            _dollarWaveText.text = Mathf.RoundToInt(dollar).ToString();
        }
    }
}