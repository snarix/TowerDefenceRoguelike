using _Source.Gameplay.BonusSystem.Abstractions;
using UnityEngine;

namespace _Source.Gameplay.UI.BonusView
{
    public interface IBonusViewFactory
    {
        BonusView Create(IBonus bonus, Transform parentTransform);
    }
}