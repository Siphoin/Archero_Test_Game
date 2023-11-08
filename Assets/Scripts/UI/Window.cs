using System;
using UnityEngine;

namespace Archero.UI
{
    public abstract class Window : MonoBehaviour
    {

        public RectTransform RectTransform { get; private set; }

        protected virtual void Awake()
        {
            if (!TryGetComponent(out RectTransform rectTransform))
            {
                throw new NullReferenceException("joysick must have component Rect Transform");
            }

            RectTransform = rectTransform;
        }

        public virtual void Exit () => gameObject.SetActive(false);
    }
}