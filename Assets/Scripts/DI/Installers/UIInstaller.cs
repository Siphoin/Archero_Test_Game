using UnityEngine;
using Zenject;
using System;
using Archero.Services;

namespace Archero.DI.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _prefab;
        public override void InstallBindings()
        {
            if (!_prefab)
            {
                throw new NullReferenceException("prefab not seted for UI Installer");
            }

            var canvas = Container.InstantiatePrefabForComponent<Canvas>(_prefab);

            Startup.GetService<UIService>().SetCanvas(canvas);
        }
    }
}