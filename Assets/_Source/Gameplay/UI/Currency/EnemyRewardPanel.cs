using System;
using TMPro;
using TowerDefenceRoguelike.Gameplay.Player;
using UnityEngine;

namespace _Source.Gameplay.UI
{
    public class EnemyRewardPanel : MonoBehaviour
    {
        [SerializeField] private DollarRewardView _dollarRewardView;
        [SerializeField] private GoldRewardView _goldRewardView;
        [SerializeField] private GameObject parent;
        
        private RewardGiver _rewardGiver;

        /*private void Start()
        {
            _rewardGiver.RewardedForDie += RewardGiverOnRewardedForDie;
        }

        private void OnDestroy()
        {
            _rewardGiver.RewardedForDie -= RewardGiverOnRewardedForDie;
        }*/

        private void RewardGiverOnRewardedForDie(float dollar, float gold)
        {
            var dollarView = Instantiate(_dollarRewardView, parent.transform.position, Quaternion.identity);
            var goldView = Instantiate(_goldRewardView, parent.transform.position, Quaternion.identity);
            
            dollarView.Initialize(_rewardGiver);
            goldView.Initialize(_rewardGiver);
            
            dollarView.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(dollar).ToString();
            goldView.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(gold).ToString();
            
            Destroy(dollarView.gameObject, 1f);
            Destroy(goldView.gameObject, 1f);
        }
    }
}