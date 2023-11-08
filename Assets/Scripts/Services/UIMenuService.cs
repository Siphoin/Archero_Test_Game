using Archero.UI;
using UnityEngine;
using System;
namespace Archero.Services
{
    public class UIMenuService : IService
    {
        private PauseWindow _pauseWindow;

        public void Initialize()
        {
            Debug.Log("ui menu service up");
        }

        public void Show ()
        {
            _pauseWindow.gameObject.SetActive(true);
        }

        public void Hide ()
        {
            _pauseWindow.gameObject.SetActive(false);
        }

        public void SetPrefab (PauseWindow pauseWindow)
        {
            if (!pauseWindow)
            {
                throw new ArgumentNullException("pause window argument is null");
            }

            _pauseWindow = pauseWindow;
        }
    }
}