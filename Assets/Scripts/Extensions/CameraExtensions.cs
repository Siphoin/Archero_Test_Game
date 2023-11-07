using UnityEngine;

namespace Archero.Extensions
{
    public static class CameraExtensions
    {
        public static Vector3 GetDesiredMovement(this Camera camera, float inputForward, float inputRight)
        {
            Vector3 directionRight = camera.transform.right;

            directionRight.y = 0f;

            directionRight.Normalize();

            Vector3 directionForward = camera.transform.forward;

            directionForward.y = 0f;

            directionForward.Normalize();

            Vector3 desiredMovement = (directionForward * inputForward) + (directionRight * inputRight);

            desiredMovement.Normalize();

            return desiredMovement;
        }
    }
}
