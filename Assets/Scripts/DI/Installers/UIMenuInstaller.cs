using Archero.Services;
using Archero.UI;
using System;
using UnityEngine;
using Zenject;

namespace Archero.DI.Installers
{
    public class UIMenuInstaller : MonoInstaller
    {
        [SerializeField] private PauseWindow _prefab;
        public override void InstallBindings()
        {
            if (!_prefab)
            {
                throw new NullReferenceException("prefab not seted for UI Menu Installer");
            }

            var prefab = Container.InstantiatePrefabForComponent<PauseWindow>(_prefab);

            prefab.gameObject.SetActive(false);

            var serviceUI = Startup.GetService<UIService>();
            serviceUI.AddElementToCanvas(prefab);

            var serviceMenuWindow = Startup.GetService<UIMenuService>();
            serviceMenuWindow.SetPrefab(prefab);
        }
    }
}