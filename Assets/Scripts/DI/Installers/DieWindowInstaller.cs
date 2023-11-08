using Archero.Services;
using Archero.UI;
using System;
using UnityEngine;
using Zenject;

namespace Archero.DI.Installers
{
    public class DieWindowInstaller : MonoInstaller
    {
        [SerializeField] private DieWindow _prefab;
        public override void InstallBindings()
        {
            if (!_prefab)
            {
                throw new NullReferenceException("prefab not seted for Die Window Installer");
            }

            var prefab = Container.InstantiatePrefabForComponent<DieWindow>(_prefab);

            prefab.gameObject.SetActive(false);

            var serviceUI = Startup.GetService<UIService>();
            serviceUI.AddElementToCanvas(prefab);
        }
    }
}