using Archero.Enemies;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Archero.Repositories
{
    public partial class LevelRepository : IRepository
    {
        private int _requireCountKill;
        private int _killCount;
        private int _moneyCollected;

        public event Action OnAllTargetsKilled;

        public event Action<int> OnKillAward;

        public void Initialize()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene currentScene, LoadSceneMode mode)
        {
            _requireCountKill = 0;
            _moneyCollected = 0;
            _killCount = 0;

            Debug.Log("current stats of level are reset");
        }

        public void Register (IEnemy enemy)
        {
            enemy.OnDeath += OnDeath;

            _requireCountKill++;
        }

        private void OnDeath(object sender, EventArgs e)
        {
            IEnemy enemy = sender as IEnemy;

            _killCount++;

            _moneyCollected += enemy.Stats.Award;

            OnKillAward?.Invoke(_moneyCollected);

            enemy.OnDeath -= OnDeath;
            if (_killCount == _requireCountKill)
            {
                OnAllTargetsKilled?.Invoke();
                Debug.Log("all targets killed. Door unlocked");
            }
        }
    }
}
