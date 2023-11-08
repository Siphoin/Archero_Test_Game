using UnityEngine;
using System;
namespace Archero
{
    [RequireComponent(typeof(MeshRenderer))]
    public class Wall : MonoBehaviour
    {
        private Renderer _renderer;

        public Bounds Bounds => _renderer.bounds;

        private void Start()
        {
            if (!TryGetComponent(out _renderer))
            {
                throw new NullReferenceException("Wall must have component Renderer");
            }
        }
    }
}