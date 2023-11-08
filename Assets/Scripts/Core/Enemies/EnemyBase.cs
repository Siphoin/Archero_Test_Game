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

namespace Archero.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class EnemyBase : MonoBehaviour, IEnemy
    {
        [SerializeField] private int _currentHealth;
        public event UnityAction<int> OnHit;
        public event EventHandler OnDealth;
        private StateMachine _stateMachine;
        private BulletPool _bulletPool;
        private NavMeshAgent _agent;
        private Rigidbody _body;
        private IPlayer _player;
        private IAnimationController _animationController;

        [SerializeField] private EnemyData _stats;

        public int Health => _currentHealth;

        public bool IsDied => _currentHealth <= 0;

        public EnemyData Stats => _stats;

        public Vector3 PositionTarget => _player.Position;

        public Vector3 Position => transform.position;

        public Transform Transform => transform;

        protected float OffsetAgent { get => _agent.baseOffset; set => _agent.baseOffset = value; }

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

            _player.OnDealth += OnDealthPlayer;

            Startup.GetRepository<LevelRepository>().Register(this);
        }

        private void OnDealthPlayer(object sender, EventArgs e)
        {
            _player.OnDealth -= OnDealthPlayer;

            _stateMachine.StopState();
        }

        private void Update()
        {
            _stateMachine?.Update();
        }

        private void FixedUpdate()
        {
            _stateMachine?.FixedUpdate();
        }

        public void Hit(int value)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - value, 0, _stats.Health);

            if (_currentHealth <= 0)
            {
                OnDealth?.Invoke(this, new DeathEventArgs());

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


        public void Shoot()
        {
           var bullet =_bulletPool.GetFreeBullet();
            bullet.SetFollowTarget(_player.Transform);
            bullet.SetPosition(transform);
            bullet.SetRotation(transform);
            
        }


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

        [Inject]
        private void Construct(IPlayer player)
        {
            _player = player;
        }
    }
}