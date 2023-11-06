using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Archero
{
    public class CustomJoystick : VariableJoystick, IJoystick
    {
        public event UnityAction OnUp;

        public event UnityAction OnDown;
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