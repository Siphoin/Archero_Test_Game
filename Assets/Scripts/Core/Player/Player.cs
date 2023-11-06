using Archero.SO;
using Archero.States;
using UnityEngine;
using System;
using Zenject;
using UnityEngine.Events;
using Archero.Bullets;

namespace Archero
{
    public class Player : MonoBehaviour, IPlayer
    {
        private int _currentHealth;
        public event UnityAction<int> OnHit;
        public event UnityAction OnDealth;
        private StateMachine _stateMachine;
        private Rigidbody _body;
        private WeaponData _weaponData;
        private BulletPool _bulletPool;
        private IJoystick _joystick;

        [SerializeField] private PlayerData _playerData;
        

        public int Health => _currentHealth;

        private void Start()
        {
            _bulletPool = new BulletPool(transform, _weaponData);

            if (!TryGetComponent(out _body))
            {
                throw new NullReferenceException("rigidbody not seted on Player");
            }

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

            _joystick.OnUp += JoystickOnUp;
            _joystick.OnDown += JoystickOnDown;

            _currentHealth = _playerData.Health;
        }

       

        private void Update()
        {
            _stateMachine?.Update();
        }

        private void FixedUpdate()
        {
            _stateMachine?.FixedUpdate();
        }

        private Vector3 GetJoystickDirection ()
        {
            return new Vector3(_joystick.Horizontal, 0f, _joystick.Vertical).normalized;
        }

        #region Moving
        public void Move()
        {
            Vector3 movement = GetJoystickDirection();
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

            Quaternion quaternion = Quaternion.LookRotation(movement);

            _body.MoveRotation(quaternion);
        }
        #endregion

        public void Hit(int value)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - value, 0, _playerData.Health);
        }

        [Inject]
        private void Construct(IJoystick joystick)
        {
            _joystick = joystick;
        }

        public void SetWeaponData(WeaponData weapon)
        {
            if (!weapon)
            {
                throw new ArgumentNullException("weapon data is null");
            }

            _weaponData = weapon;
        }

        public void Shoot()
        {
            var bullet =_bulletPool.GetFreeBullet();
            bullet.SetPosition(transform);
            bullet.SetRotation(transform);
        }
    }
}