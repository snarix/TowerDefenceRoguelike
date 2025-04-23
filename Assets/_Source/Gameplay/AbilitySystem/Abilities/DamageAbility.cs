using System;
using System.Collections;
using _Source.Gameplay.AbilitySystem.Abstractions;
using _Source.Gameplay.AbilitySystem.Data;
using Include;
using TowerDefenceRoguelike.Gameplay.Player;
using UnityEngine;

namespace _Source.Gameplay.AbilitySystem.Abilities
{
    public class DamageAbility : CooldownAbility, IDisposable
    {
        private PlayerStats _playerStats;
        private ICoroutineHandler _coroutineHandler;
        private Coroutine _coroutine;
        private float _value;
        private float _duration;

        public DamageAbility(AbilityData abilityData, float value, float cooldown, float duration,
            PlayerStats playerStats, ICoroutineHandler coroutineHandler) : base(abilityData, cooldown)
        {
            _value = value;
            _duration = duration;
            _playerStats = playerStats;
            _coroutineHandler = coroutineHandler;
        }

        public override AbilityType Type => AbilityType.DamageAbility;

        public void Dispose()
        {
            if (_coroutine != null)
                _coroutineHandler.StopCoroutine(_coroutine);
        }

        protected override void OnUsed()
        {
            _playerStats.Damage.Value += Mathf.RoundToInt(_value);
            Debug.Log($"Урон увеличен до {_playerStats.Damage}. Оставшееся время: {_duration}");
            _coroutine = _coroutineHandler.StartCoroutine(DamageCoroutine());
        }

        private IEnumerator DamageCoroutine()
        {
            yield return new WaitForSeconds(_duration);
            RemoveDamageBonus();
        }

        private void RemoveDamageBonus()
        {
            _playerStats.Damage.Value -= Mathf.RoundToInt(_value);
            Debug.Log($"Эффект закончился. Урон вернулся к {_playerStats.Damage}");
        }
    }
}