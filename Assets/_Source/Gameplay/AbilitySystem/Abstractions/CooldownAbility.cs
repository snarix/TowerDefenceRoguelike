using _Source.Gameplay.AbilitySystem.Data;
using _Source.Gameplay.UI.BonusView;
using UnityEngine;

namespace _Source.Gameplay.AbilitySystem.Abstractions
{
    public abstract class CooldownAbility : IAbility
    {
        private float _cooldown;
        private float _nextCooldown = 0f;
         
        protected CooldownAbility(AbilityData abilityData, float cooldown)
        {
            Data = abilityData;
            _cooldown = cooldown;
        }

        public abstract AbilityType Type { get; }
        
        public AbilityData Data { get; }

        public void Accept(IAbilityVisitor visitor)
        {
            visitor.Visit(this);
        }
        
        public bool CanBeUsed() => Time.time >= _nextCooldown;

        public float GetRemainingTime()
        {
            if (_nextCooldown <= Time.time)
            {
                return _cooldown;
            }
            return _nextCooldown - Time.time;
        } 
            
        
        public float GetCooldown() => _cooldown;
        
        public void Use()
        {
            if (CanBeUsed())
            {
                _nextCooldown = Time.time + _cooldown;
                OnUsed();
            }
        }
        
        protected abstract void OnUsed();
    }
}