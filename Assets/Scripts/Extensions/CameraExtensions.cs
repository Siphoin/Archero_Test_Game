using UnityEngine;

namespace Archero.Extensions
{
    public static class CameraExtensions
    {
        public static Vector3 GetDesiredMovement(this Camera camera, float inputForward, float inputRight)
        {
            Vector3 desiredMovement = (ToZeroDirection(camera.transform.forward) * inputForward) + (ToZeroDirection(camera.transform.right) * inputRight);

            desiredMovement.Normalize();

            return desiredMovement;
        }

        private static Vector3 ToZeroDirection (Vector3 direction)
        {
            direction.y = 0f;
            direction.Normalize();
            return direction;
        }
    }
}
