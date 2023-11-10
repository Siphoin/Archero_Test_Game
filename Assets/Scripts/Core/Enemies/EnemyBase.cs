using Archero.Bullets;
using Archero.SO;
using Archero.States;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Zenject;
using System;
using Archero.Enemies.States;
using Archero.Repositories;
using Archero.Animation;
using Archero.Services;

namespace Archero.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class EnemyBase : MonoBehaviour, IEnemy
    {
        [SerializeField] private int _currentHealth;

        private RigidbodyConstraints _defaultBodyConstraints;
        private bool _isActive;
        public event UnityAction<int> OnHit;
        public event EventHandler OnDeath;
        private StateMachine _stateMachine;
        private BulletPool _bulletPool;
        private NavMeshAgent _agent;
        private Rigidbody _body;

        private LevelService _levelService;

        [SerializeField] private EnemyData _stats;

        private IPlayer _player;

        private IAnimationController _animationController;

        public int Health => _currentHealth;

        protected float OffsetAgent { get => _agent.baseOffset; set => _agent.baseOffset = value; }

        public bool IsDied => _currentHealth <= 0;

        public bool IsActive => _isActive && _stateMachine.Current != _stateMachine.DefaultState;

        public EnemyData Stats => _stats;

        public Vector3 PositionTarget => _player.Position;

        public Vector3 Position => transform.position;

        public Transform Transform => transform;

        protected virtual void Start()
        {
            if (!TryGetComponent(out _agent))
            {
                throw new NullReferenceException("enemy must have cvomponent Nav Mesh Agent");
            }

            if (!TryGetComponent(out _body))
            {
                throw new NullReferenceException("enemy must have component Rigidbody");
            }

            if (!TryGetComponent(out _animationController))
            {
                Debug.LogWarning($"animation controller not found on Enemy {name}");
            }

            _bulletPool = new BulletPool(transform, _stats.Weapon);

            InitializeStates();

            _agent.acceleration = float.MaxValue;
            _agent.stoppingDistance = _stats.DistanceMove;
            _agent.updateRotation = false;

            _currentHealth = _stats.Health;

            _player.OnDeath += OnDeathPlayer;

            _defaultBodyConstraints = _body.constraints;

            _levelService = Startup.GetService<LevelService>();

            _levelService.OnTickEnd += OnTickEnd;

            Startup.GetRepository<LevelRepository>().Register(this);
        }

        private void Update()
        {
            _stateMachine?.Update();
        }

        private void FixedUpdate()
        {
            _stateMachine?.FixedUpdate();
        }

        private void OnTickEnd()
        {
            _levelService.OnTickEnd -= OnTickEnd;

            Activate();
        }

        private void OnDeathPlayer(object sender, EventArgs e)
        {
            _player.OnDeath -= OnDeathPlayer;

            _stateMachine.StopState();
        }

        public void Hit(int value)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - value, 0, _stats.Health);

            if (_currentHealth <= 0)
            {
                OnDeath?.Invoke(this, new DeathEventArgs());

                _stateMachine.StopState();

                Hide();

            }
        }

        #region States
        protected virtual void InitializeStates ()
        {
            var states = new OwneringState<IEnemy>[]
           {
                new StayEnemyState(),
                new MoveEnemyState(),
                new ShootEnemyState(),
           };

            foreach (var state in states)
            {
                state.SetOwner(this);
            }

            _stateMachine = new StateMachine();
            _stateMachine.Initialize(states);

        }

        public void BeginToShoot()
        {
            _stateMachine.SetState<ShootEnemyState>();
        }

        public void BeginMove()
        {
            _stateMachine.SetState<MoveEnemyState>();
        }
        #endregion

        #region Moving
        public void Move()
        {
            _agent.SetDestination(_player.Position);

            _body.angularVelocity = Vector3.zero;

            _body.velocity = Vector3.zero;
        }

        #endregion

        #region Rotating
        public void Rotate()
        {
            Vector3 direction = PositionTarget - Position;

            Quaternion rotation = Quaternion.LookRotation(direction);

            transform.rotation = rotation;
        }
        #endregion


        public void Shoot() => _bulletPool.GetFreeBullet(transform, _player.Transform);


        public void Hide()
        {
            if (_animationController != null)
            {
                _animationController.OnEnd += OnEndAnimations;
                _animationController.Play();
            }

            else
            {
                gameObject.SetActive(false);
            }
        }

        private void OnEndAnimations()
        {
            _animationController.OnEnd -= OnEndAnimations;

            gameObject.SetActive(false);
        }

        public void Activate()
        {
            _isActive = true;

            _stateMachine.SetStateByDefault();

            _body.constraints = _defaultBodyConstraints;

            _agent.isStopped = false;
        }

        public void Deactivate()
        {
            _isActive = false;

            _stateMachine.StopState();

            _body.constraints = RigidbodyConstraints.FreezeAll;

            _agent.isStopped = true;
        }

        [Inject]
        private void Construct(IPlayer player)
        {
            _player = player;
        }
    }
}