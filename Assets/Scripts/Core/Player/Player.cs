using Archero.SO;
using Archero.States;
using UnityEngine;
using System;
using Zenject;
using UnityEngine.Events;
using Archero.Bullets;
using Archero.Extensions;
using Archero.Services;
using Archero.Animation;

namespace Archero
{
    public class Player : MonoBehaviour, IPlayer
    {
        [SerializeField] private int _currentHealth;
        private BulletType _currentBehaviourShooting = BulletType.Ground;
        public event UnityAction<int> OnHit;
        public event EventHandler OnDealth;
        private StateMachine _stateMachine;
        private Rigidbody _body;
        private WeaponPlayerData _weaponData;
        private BulletPool _bulletPool;
        private IJoystick _joystick;
        private RigidbodyConstraints _defaultBodyConstraints;

        [SerializeField] private PlayerData _playerData;

        private Transform _targetBullets;

        private LevelService _levelService;

        private IAnimationController _animationController;

        public int Health => _currentHealth;

        public bool IsDied => _currentHealth <= 0;

        public bool IsActive { get; protected set; }

        public Vector3 Position => transform.position;

        public Vector3 Forward => transform.forward;

        public WeaponPlayerData Weapon => _weaponData;

        public Transform Transform => transform;

        private void Start()
        {
            _bulletPool = new BulletPool(transform, _weaponData);

            if (!TryGetComponent(out _body))
            {
                throw new NullReferenceException("rigidbody not seted on Player");
            }


            if (!TryGetComponent(out _animationController))
            {
                Debug.LogWarning($"animation controller not found on Enemy {name}");
            }

            InitializeStates();

            _defaultBodyConstraints = _body.constraints;

            _joystick.OnUp += JoystickOnUp;
            _joystick.OnDown += JoystickOnDown;

            _currentHealth = _playerData.Health;

            _levelService = Startup.GetService<LevelService>();

            _levelService.OnTickEnd += OnTickEnd;
        }

        private void Update()
        {
            _stateMachine?.Update();
        }

        private void FixedUpdate()
        {
            _stateMachine?.FixedUpdate();
        }

        private void InitializeStates()
        {
            var states = new OwneringState<IPlayer>[]
            {
                new ShootPlayerState(),
                new ControlPlayerState()
            };

            foreach (var state in states)
            {
                state.SetOwner(this);
            }

            _stateMachine = new StateMachine();
            _stateMachine.Initialize(states);
        }

        private void OnTickEnd()
        {
            _levelService.OnTickEnd -= OnTickEnd;

            Activate();
        }

        private Vector3 GetJoystickDirection()
        {
            return new Vector3(_joystick.Horizontal, 0f, _joystick.Vertical).normalized;
        }

        #region Moving
        public void Move()
        {
            Vector3 movement = GetJoystickDirection();
            movement = Camera.main.GetDesiredMovement(movement.z, movement.x);

            _body.velocity = movement * _playerData.SpeedMovement;
        }

        public void StopMove()
        {
            _body.velocity = Vector3.zero;
        }

        private void JoystickOnUp()
        {
            _stateMachine.SetStateByDefault();
        }

        private void JoystickOnDown()
        {
            _stateMachine.SetState<ControlPlayerState>();
        }

        #endregion

        #region Rotating
        public void Rotate()
        {
            Vector3 movement = GetJoystickDirection();

            movement = Camera.main.GetDesiredMovement(movement.z, movement.x);

            Quaternion quaternion = Quaternion.LookRotation(movement);

            _body.MoveRotation(quaternion);
        }
        #endregion

        public void Hit(int value)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - value, 0, _playerData.Health);

            if (_currentHealth <= 0)
            {
                _joystick.OnUp -= JoystickOnUp;
                _joystick.OnDown -= JoystickOnDown;

                _stateMachine.StopState();

                Hide();

                OnDealth?.Invoke(this, new DeathEventArgs());
            }
        }

        public void SetWeaponData(WeaponPlayerData weapon)
        {
            if (!weapon)
            {
                throw new ArgumentNullException("weapon data is null");
            }

            _weaponData = weapon;
        }

        public void Shoot()
        {
            var bullet = _bulletPool.GetFreeBullet();
            bullet.SetBehaviour(_currentBehaviourShooting);
            bullet.SetFollowTarget(_targetBullets);
            bullet.SetPosition(transform);
            bullet.SetRotation(transform);
        }

        private void OnDrawGizmos()
        {
            if (_weaponData != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position, _weaponData.Radius);
            }
        }

        public void Rotate(Vector3 position)
        {
            Vector3 direction = position - transform.position;

            Quaternion root = Quaternion.LookRotation(direction);

            Quaternion rotate = root;

            rotate.x = 0;

            rotate.z = 0;

            transform.rotation = rotate;
        }

        public void SetBehaviourShoot(BulletType behavipurBullet)
        {
            _currentBehaviourShooting = behavipurBullet;
        }

        public void SetTargetForBullets(Transform target)
        {
            _targetBullets = target;
        }

        public void Activate()
        {
            IsActive = true;

            _stateMachine.SetStateByDefault();

            _body.constraints = _defaultBodyConstraints;
        }

        public void Deactivate()
        {
            IsActive = false;

            _stateMachine.StopState();

            _body.constraints = RigidbodyConstraints.FreezeAll;
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
        private void Construct(IJoystick joystick)
        {
            _joystick = joystick;
        }
    }

}