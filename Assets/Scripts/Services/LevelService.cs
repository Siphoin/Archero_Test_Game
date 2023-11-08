using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Archero.Services
{
    public class LevelService : IService
    {
        private readonly int _timeOutStart = 3;
        private readonly float _delayStart = 0.5f;

        public event Action<int> OnTick;
        public event Action OnTickEnd;

        public bool IsStarted { get; private set; }
             
        public void Initialize()
        {
            Debug.Log("level service up");

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            IsStarted = false;
            StartLevel();
        }

        private async void StartLevel()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(_delayStart);

            await UniTask.Delay(timeSpan);

            TickStart().Forget();
        }

        public void RestartLevel ()
        {
            var thisScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(thisScene);
        }

        private async UniTask TickStart ()
        {
            int tick = 0;

            TimeSpan timeSpan = TimeSpan.FromSeconds(1);

            while (tick < _timeOutStart + 1)
            {
                await UniTask.Delay(timeSpan);

                tick++;

                OnTick?.Invoke(tick);

                await UniTask.Yield();
            }

            IsStarted = true;

            OnTickEnd?.Invoke();

            Debug.Log("level started");
        }
    }
}
