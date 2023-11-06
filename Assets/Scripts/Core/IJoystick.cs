using UnityEngine.Events;

namespace Archero
{
    public interface IJoystick
    {
        event UnityAction OnUp;
        event UnityAction OnDown;

        float Vertical { get; }
        float Horizontal { get; }
    }
}
