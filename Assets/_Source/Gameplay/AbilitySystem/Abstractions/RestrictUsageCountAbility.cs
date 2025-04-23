using _Source.Gameplay.AbilitySystem.Data;
using _Source.Gameplay.UI.BonusView;

namespace _Source.Gameplay.AbilitySystem.Abstractions
{
    public abstract class RestrictUsageCountAbility : IAbility
    {
        private int _currentUsageCount;
        private int _maximumUsageCount;

        protected RestrictUsageCountAbility(AbilityData abilityData, int maximumUsageCount)
        {
            Data = abilityData;
            _maximumUsageCount = maximumUsageCount;
            _currentUsageCount = _maximumUsageCount;
        }

        public abstract AbilityType Type { get; }
        
        public AbilityData Data { get; }
        public bool CanBeUsed() => _currentUsageCount > 0;
        
        public int GetCurrentUsageCount() => _currentUsageCount;
        
        public void Accept(IAbilityVisitor visitor)
        {
            visitor.Visit(this);
        }
        
        public void Use()
        {
            if (CanBeUsed())
            {
                _currentUsageCount--;
                OnUse();
            }
        }
        
        protected abstract void OnUse();
        
        protected void IncreaseUsageCount()
        {
            if(_currentUsageCount < _maximumUsageCount)
                _currentUsageCount++;
        }
    }
}