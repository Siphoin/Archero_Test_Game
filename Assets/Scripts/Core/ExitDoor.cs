using Archero.Repositories;
using UnityEngine;
using System;
using Archero.Services;
using DG.Tweening;

namespace Archero.Core
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(Rigidbody))]
    public class ExitDoor : MonoBehaviour
    {
        private bool _canExit;

        [SerializeField, Min(0)] private float _durationChangeColor = 0.5F;

        private Renderer _renderer;
        private LevelRepository _levelRepository;
        private LevelService _levelService;

        private void Awake()
        {
            _levelRepository = Startup.GetRepository<LevelRepository>();
            _levelService = Startup.GetService<LevelService>();
        }

        private void Start()
        {
            if (!TryGetComponent(out _renderer))
            {
                throw new NullReferenceException("exit door must have component Renderer");
            }

            SetColorDoor(Color.red);
        }

        private void OnAllTargetsKilled()
        {
            _levelRepository.OnAllTargetsKilled -= OnAllTargetsKilled;

            SetColorDoor(Color.green);

            _canExit = true;
        }

        private void SetColorDoor (Color color)
        {
            _renderer.material.DOColor(color, _durationChangeColor);
        }

        private void OnEnable()
        {
            _levelRepository.OnAllTargetsKilled += OnAllTargetsKilled;
        }

        private void OnDestroy()
        {
            _levelRepository.OnAllTargetsKilled -= OnAllTargetsKilled;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IPlayer _) && _canExit)
            {
                _levelService.RestartLevel();
            }
        }
    }
}