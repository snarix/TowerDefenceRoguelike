using System;
using _Source.Gameplay.Currency;
using _Source.Gameplay.UI.HealthView;
using TowerDefenceRoguelike.Gameplay.Base;
using TowerDefenceRoguelike.Gameplay.Enemy.Factory;
using TowerDefenceRoguelike.Gameplay.Enemy.StateMachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace TowerDefenceRoguelike.Gameplay.Enemy
{
    [DefaultExecutionOrder(900)]
    public class Enemy : MonoBehaviour
    {
        [FormerlySerializedAs("mover")] [SerializeField] private EnemyMover _mover;
        [FormerlySerializedAs("attacker")] [SerializeField] private EnemyAttacker _attacker;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private Health _health;
        [SerializeField] private HealthView _healthView;
        [SerializeField] private TextDamageView _textDamageView;
        [SerializeField] private EnemyAnimationEventsHandler _animationHandler;
        [SerializeField] private EnemyType _type;
        [SerializeField] private Collider _collider;
        [SerializeField] private EnemyStats _stats;

        private Health _attackTarget;
        private EnemyStateMachine _stateMachine;
        private EnemySpawner _spawner;

        public EnemyMover Mover => _mover;
        public EnemyAttacker Attacker => _attacker;
        public EnemyAnimator Animator => _animator;
        
        public Collider Collider => _collider;

        public Health AttackTarget => _attackTarget;

        public HealthView HealthView => _healthView;

        public Vector3 Target => _attackTarget.transform.position;

        public EnemyAnimationEventsHandler AnimationHandler => _animationHandler;

        public EnemyType Type => _type;

        public float DestructionDelay => 3f;

        public event Action<Enemy> Died;
        public event Action<int> EnemyHealthUpdated;
        
        public void Initialize(Health attackTarget, EnemySpawner enemySpawner)
        {
            _attackTarget = attackTarget;
            _stateMachine = new EnemyStateMachine(this);
            
            _spawner = enemySpawner;
            _spawner.EnemySpawned += OnEnemySpawned;
            
            _healthView.Initialize(_health);    
            _textDamageView.Initialize(_health);
        }
        
        private void OnEnemySpawned(int currentWaveIndex)
        {
            int waveBonus = Mathf.Max(0, currentWaveIndex - _stats.InFirstWave);
            float newDamage = _stats.Damage + _stats.AmountDamage * waveBonus;
            int newHealth = _stats.MaxHealth + _stats.AmountHealth * waveBonus;
            
            _attacker.Initialize(new EnemyStatsTemp(newDamage, newHealth));
            _health.SetMaxHealth(newHealth);
            
            if (_type == EnemyType.Default)
                EnemyHealthUpdated?.Invoke(newHealth);
        }

        private void OnEnable()
        {
            if (_attackTarget != null)
                _attackTarget.Died += OnAttackTargetDied;
            _health.Died += OnHealthDied;
        }

        private void OnDisable()
        {
            _attackTarget.Died -= OnAttackTargetDied;
            _health.Died -= OnHealthDied;
            _spawner.EnemySpawned -= OnEnemySpawned;
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        private void OnAttackTargetDied(Health health)
        {
            _stateMachine.SwitchState<IdleState>();
        }

        private void OnHealthDied(Health health)
        {
            _stateMachine.SwitchState<DeathState>();
            Died?.Invoke(this);
        }
    }
}