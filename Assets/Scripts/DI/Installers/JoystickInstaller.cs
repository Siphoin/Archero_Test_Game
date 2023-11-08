using UnityEngine;
using System;
using Zenject;
using Archero.Services;
using Archero.UI;

namespace Archero.DI.Installers
{
    public class JoystickInstaller : MonoInstaller
    {
        [SerializeField] private CustomJoystick _prefab;

        public override void InstallBindings()
        {
            if (!_prefab)
            {
                throw new NullReferenceException("prefab joystick not seted on installer");
            }

            var joystick = Container.InstantiatePrefabForComponent<CustomJoystick>(_prefab, _prefab.transform.position, Quaternion.identity, null);
            Startup.GetService<UIService>().AddElementToCanvas(joystick.RectTransform);
            Vector3 positionJoystik = joystick.transform.localPosition;
            positionJoystik.x = 0;
            joystick.transform.localPosition = positionJoystik;
            joystick.transform.SetSiblingIndex(0);
            Container.Bind<IJoystick>().To<CustomJoystick>().FromInstance(joystick);
        }
    }
}