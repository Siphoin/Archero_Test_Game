using UnityEngine;
using System;

namespace Archero.Bullets
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour, IBullet
    {
        [SerializeField]  private BulletOwner _owner;

        [SerializeField, Min(1)] private int _damage = 1;

        private Rigidbody _body;

        [SerializeField, Min(1)] private float _speed = 10;

        private void Awake()
        {
            if (!TryGetComponent(out _body))
            {
                throw new NullReferenceException("bullet must have component Rigidbody");
            }
        }

        private void FixedUpdate()
        {
            _body.velocity = transform.forward * _speed;
            _body.angularVelocity = Vector3.zero;
        }

        private void OnCollisionEnter(Collision collision)
        {
            GameObject gameObject = collision.gameObject;


            if (gameObject.TryGetComponent(out Wall _))
            {
                Hide();
            }

            if (_owner == BulletOwner.Player)
            {

                if (gameObject.TryGetComponent(out IPlayer player))
                {
                    player.Hit(_damage);

                    Hide();
                }
            }
              
        }

        private void Hide()
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
    }
}