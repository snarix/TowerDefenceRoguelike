using System;
using System.Collections;
using _Source.Gameplay.AbilitySystem.Abstractions;
using _Source.Gameplay.AbilitySystem.Data;
using Include;
using TowerDefenceRoguelike.Gameplay.Player;
using UnityEngine;

namespace _Source.Gameplay.AbilitySystem.Abilities
{
    public class RadiusAbility : CooldownAbility, IDisposable
    {
        private PlayerStats _playerStats;
        private ICoroutineHandler _coroutineHandler;
        private Coroutine _coroutine;
        private float _value;
        private float _duration;

        public RadiusAbility(AbilityData abilityData, float value, float cooldown, float duration,
            PlayerStats playerStats, ICoroutineHandler coroutineHandler) : base(abilityData, cooldown)
        {
            _value = value;
            _duration = duration;
            _playerStats = playerStats;
            _coroutineHandler = coroutineHandler;
        }

        public override AbilityType Type => AbilityType.RadiusAbility;

        public void Dispose()
        {
            if (_coroutine != null)
                _coroutineHandler.StopCoroutine(_coroutine);
        }

        protected override void OnUsed()
        {
            _playerStats.Radius.Value += _value;
            Debug.Log($"Радиус увеличен до {_playerStats.Radius.Value}. Оставшееся время: {_duration}");
            _coroutine = _coroutineHandler.StartCoroutine(RadiusCoroutine());
        }

        private IEnumerator RadiusCoroutine()
        {
            yield return new WaitForSeconds(_duration);
            UnUsed();
        }

        private void UnUsed()
        {
            _playerStats.Radius.Value -= _value;
            Debug.Log($"Эффект закончился. Радиус вернулся к {_playerStats.Radius.Value}");
        }
    }
}