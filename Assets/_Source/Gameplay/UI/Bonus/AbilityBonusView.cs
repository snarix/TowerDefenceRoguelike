using _Source.Gameplay.BonusSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.Gameplay.UI.BonusView
{
    public class AbilityBonusView : BonusView
    {
        [SerializeField] private TextMeshProUGUI _bonusNameText;
        [SerializeField] private TextMeshProUGUI _bonusDescriptionText;

        public void Initialize(BonusType bonusType, string bonusName, string bonusDescription)
        {
            _bonusType = bonusType;
            
            _bonusNameText.text = bonusName;
            _bonusDescriptionText.text = bonusDescription;
        }
    }
}