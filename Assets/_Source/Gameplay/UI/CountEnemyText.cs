using TMPro;
using UnityEngine;

namespace _Source.Gameplay.UI
{
    public class CountEnemyText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _enemyCountText;

        //public void UpdateEnemyCount(int count) => _enemyCountText.text = $"Enemies Remaining: {count}";
        public void UpdateEnemyCount(int count) => _enemyCountText.text = count.ToString();
    }
}