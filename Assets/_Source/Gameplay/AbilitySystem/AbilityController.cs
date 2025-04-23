using System;
using System.Collections.Generic;
using System.Linq;
using _Source.Gameplay.AbilitySystem.Abilities;
using _Source.Gameplay.AbilitySystem.Abstractions;
using _Source.Gameplay.AbilitySystem.Data;
using _Source.Gameplay.UI.Ability;
using Include;
using TowerDefenceRoguelike.Gameplay.Enemy.Factory;
using TowerDefenceRoguelike.Gameplay.Player;
using TowerDefenceRoguelike.Gameplay.Player.Abstractions;

namespace _Source.Gameplay.AbilitySystem
{
    public class AbilityController : IDisposable
    {
        private List<IAbility> _abilities;
        private AbilityView _abilityView;
        private DamageInAreaParticleView _damageInAreaParticleView;

        public AbilityController(PlayerStats playerStats, AbilityConfigHolder abilityData, AbilityView abilityView,
            ICoroutineHandler handler, IEnemiesFinder enemyFinder, DamageInAreaView damageInAreaView,
            EnemySpawner enemySpawner, ZoneChooser zoneChooser, DamageInAreaParticleView damageInAreaParticleView)
        {
            _abilities = new List<IAbility>
            {
                new DamageAbility(
                    abilityData.GetAbilityData<CooldownData>(AbilityType.DamageAbility),
                    abilityData.GetAbilityData<CooldownData>(AbilityType.DamageAbility).Value,
                    abilityData.GetAbilityData<CooldownData>(AbilityType.DamageAbility).Cooldown,
                    abilityData.GetAbilityData<CooldownData>(AbilityType.DamageAbility).Duration,
                    playerStats,
                    handler),

                /*new RadiusAbility(
                    abilityData.GetAbilityData<CooldownData>(AbilityType.RadiusAbility),
                    abilityData.GetAbilityData<CooldownData>(AbilityType.RadiusAbility).Value,
                    abilityData.GetAbilityData<CooldownData>(AbilityType.RadiusAbility).Cooldown,
                    abilityData.GetAbilityData<CooldownData>(AbilityType.RadiusAbility).Duration,
                    playerStats,
                    handler),*/

                new DamageInArea(
                    abilityData.GetAbilityData<CountData>(AbilityType.DamageInAreaAbility),
                    abilityData.GetAbilityData<CountData>(AbilityType.DamageInAreaAbility).Count,
                    abilityData.GetAbilityData<CountData>(AbilityType.DamageInAreaAbility).Radius,
                    enemyFinder,
                    playerStats,
                    zoneChooser,
                    enemySpawner,
                    damageInAreaView)
            };

            _abilityView = abilityView;
            _damageInAreaParticleView = damageInAreaParticleView;
            _abilityView.AbilityApply += OnAbilityApply;
        }

        public void Dispose()
        {
            foreach (var disposable in _abilities.Where(x => x is IDisposable).Cast<IDisposable>())
            {
                disposable.Dispose();
            }

            _abilityView.AbilityApply -= OnAbilityApply;
        }

        public void CreateAbility()
        {
            _abilityView.CreateAbility(_abilities);
            var damageInArea = _abilities.FirstOrDefault(x => x.Type == AbilityType.DamageInAreaAbility) as DamageInArea;
            _damageInAreaParticleView.Initialize(damageInArea);
        }

        private void OnAbilityApply(AbilityType abilityType)
        {
            var ability = _abilities.FirstOrDefault(x => x.Type == abilityType);
            ability?.Use();
        }
    }
}