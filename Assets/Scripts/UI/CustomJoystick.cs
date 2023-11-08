using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using Archero.Services;

namespace Archero.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class CustomJoystick : VariableJoystick, IJoystick
    {
        public event UnityAction OnUp;
        public event UnityAction OnDown;

        private LevelService _levelService;

        public RectTransform RectTransform {  get; private set; }

        private void Awake()
        {
            if (!TryGetComponent(out RectTransform rectTransform))
            {
                throw new NullReferenceException("joysick must have component Rect Transform");
            }

            RectTransform = rectTransform;

            _levelService = Startup.GetService<LevelService>();

            _levelService.OnTickEnd += OnLevelStart;

            gameObject.SetActive(false);
        }

        private void OnLevelStart()
        {
            _levelService.OnTickEnd -= OnLevelStart;

            gameObject.SetActive(true);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            OnUp?.Invoke();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            OnDown?.Invoke();
        }
    }
}