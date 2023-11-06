using UnityEngine;
using System;
using Zenject;

namespace Archero.DI.Installers
{
    public class JoystickInstaller : MonoInstaller
    {
        [SerializeField] private CustomJoystick _prefab;
        [SerializeField] private Canvas _canvas;

        public override void InstallBindings()
        {
            if (!_prefab)
            {
                throw new NullReferenceException("prefab joystick not seted on installer");
            }

            if (!_canvas)
            {
                throw new NullReferenceException("canvas not seted on joystick installer");
            }

            var joystick = Container.InstantiatePrefabForComponent<CustomJoystick>(_prefab, _prefab.transform.position, Quaternion.identity, _canvas.transform);
            Container.Bind<IJoystick>().To<CustomJoystick>().FromInstance(joystick);
        }
    }
}