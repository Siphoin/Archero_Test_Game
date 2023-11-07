﻿using UnityEngine;
using System;
using Archero.Enemies;

namespace Archero.Bullets
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour, IBullet
    {
        [SerializeField]  private BulletOwner _owner;
        [SerializeField] private BulletType _type;

        private Transform _parent;

        [SerializeField, Min(1)] private int _damage = 1;

        private Rigidbody _body;

        [SerializeField, Min(1)] private float _speed = 10;
        private Transform _target;

        private void Awake()
        {
            if (!TryGetComponent(out _body))
            {
                throw new NullReferenceException("bullet must have component Rigidbody");
            }

            _parent = transform.parent;
        }

        private void FixedUpdate()
        {
            Vector3 direction = transform.forward;

            if (_type == BulletType.Ground)
            {
                _body.velocity = direction * _speed;
                
            }

            else if (_type == BulletType.ToTarget)
            {
                if (_target is null)
                {
                    throw new InvalidOperationException("target for bullet not seted");
                }

                direction = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.fixedDeltaTime);
                transform.position = direction;
            }

          _body.angularVelocity = Vector3.zero;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Wall _))
            {
                Hide();
            }

            if (_owner == BulletOwner.Player)
            {

                if (other.TryGetComponent(out IEnemy enemy))
                {
                    enemy.Hit(_damage);

                    Hide();
                }
            }

            if (_owner == BulletOwner.Enemy)
            {

                if (other.TryGetComponent(out IPlayer player))
                {
                    player.Hit(_damage);

                    Hide();
                }
            }

        }

        public void Hide()
        {
            gameObject.SetActive(false);

            // TODO: add hiding animation
        }

        public void SetPosition(Transform root)
        {
            transform.position = root.position;
        }

        public void SetRotation(Transform root)
        {
            transform.rotation = root.rotation;
        }

        public void SetBehaviour(BulletType type)
        {
            _type = type;
        }

        public void SetFollowTarget(Transform target)
        {
            _target = target;
        }

        private void OnDisable()
        {
            _target = null;

            transform.localPosition = Vector3.zero;
        }
    }
}