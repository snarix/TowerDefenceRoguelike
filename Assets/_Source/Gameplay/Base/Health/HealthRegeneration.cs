using System;
using System.Collections;
using Include;
using UnityEngine;

namespace TowerDefenceRoguelike.Gameplay.Base
{
    public class HealthRegeneration : IDisposable
    {
        private Health _health;
        private ICoroutineHandler _coroutineHandler;
        private Coroutine _coroutine;
        private float _cooldown = 1f;

        public HealthRegeneration(Health health, ICoroutineHandler coroutineHandler)
        {
            _health = health;
            _coroutineHandler = coroutineHandler;
        }

        public void Activate(float amount)
        {
            Dispose(); //?

            _coroutine = _coroutineHandler.StartCoroutine(RegenerationRoutine(amount));
            Debug.Log("Health Regeneration activated - " + amount);
            _health.Died += OnHealthDied;
        }

        private void Deactivate()
        {
            _coroutineHandler.StopCoroutine(_coroutine);
            Debug.Log("Health Regeneration deactivated");
        }

        public void Dispose()
        {
            _health.Died -= OnHealthDied;
            if (_coroutine != null)
                Deactivate();
        }

        private IEnumerator RegenerationRoutine(float amount)
        {
            while (true)
            {
                _health.Heal(amount);
                yield return new WaitForSeconds(_cooldown);
            }
        }

        private void OnHealthDied(Health health)
        {
            Dispose();
        }
    }
}