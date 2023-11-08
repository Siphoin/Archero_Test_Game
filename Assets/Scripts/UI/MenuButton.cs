using UnityEngine;
using System;
using UnityEngine.UI;
using Archero.Services;

namespace Archero.UI
{
    [RequireComponent(typeof(Button))]
    public class MenuButton : MonoBehaviour
    {
        private Button _button;

        private UIMenuService _service;

        private void Awake()
        {
            _service = Startup.GetService<UIMenuService>();
        }

        private void Start()
        {
            if (!TryGetComponent(out _button))
            {
                throw new NullReferenceException("menu button must have component UI Button");
            }

            _button.onClick.AddListener(Open);
        }

        private void Open()
        {
            _service.Show();
        }
    }
}