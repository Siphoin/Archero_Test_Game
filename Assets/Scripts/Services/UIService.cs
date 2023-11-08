using Archero.UI;
using UnityEngine;
using System;
namespace Archero.Services
{
    public class UIService : IService
    {
        private Canvas _canvas;

        public void Initialize()
        {
            Debug.Log("ui service up");
        }

        public void SetCanvas (Canvas canvas)
        {
            if (!canvas)
            {
                throw new ArgumentNullException("canvas argument null");
            }

            _canvas = canvas;
        }

        public void AddElementToCanvas (RectTransform rectTransform)
        {
            if (!_canvas)
            {
                throw new NullReferenceException("canvas not seted");
            }

            if (!rectTransform)
            {
                throw new ArgumentNullException("rect transform target null");
            }

            rectTransform.SetParent(_canvas.transform, false);
        }

        public void AddElementToCanvas(Window window)
        {
            AddElementToCanvas(window.RectTransform);
        }
    }
}