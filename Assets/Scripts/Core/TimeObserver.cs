using System.Collections;
using UnityEngine;
using System;
using Archero.Services;

namespace Archero.Core
{
    public class TimeObserver : MonoBehaviour
    {
        private TimeService _timeService;
        private IActivatable _activatableObject;

        private void Awake()
        {
            _timeService = Startup.GetService<TimeService>();

          
        }

        private void Start()
        {
            if (!TryGetComponent(out _activatableObject))
            {
                throw new NullReferenceException($"time observer only work with {nameof(IActivatable)} interface");
            }
            var levelService = Startup.GetService<LevelService>();

            if (!levelService.IsStarted)
            {
                _activatableObject.Deactivate();
            }
            
        }

        private void OnEnable()
        {
            _timeService.OnPause += OnPause;
            _timeService.OnResume += OnResume;
        }

        private void OnDisable()
        {
            _timeService.OnPause -= OnPause;
            _timeService.OnResume -= OnResume;
        }

        private void OnResume()
        {
            _activatableObject.Activate();
        }

        private void OnPause()
        {
            _activatableObject.Deactivate();
        }

        
    }
}