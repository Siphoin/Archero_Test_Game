using System;
using UnityEngine;

namespace Archero.Services
{
    public class TimeService : IService
    {
        public event Action OnPause;
        public event Action OnResume;
        public void Initialize()
        {
            Debug.Log("time service up");
        }

        internal void Pause()
        {
            OnPause?.Invoke();
        }

        internal void Resume()
        {
            OnResume?.Invoke();
        }
    }
}
