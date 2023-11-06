using UnityEngine;

namespace Archero
{
    public class SpawnPoint : MonoBehaviour
    {
        public Vector3 GetPosition ()
        {
            return transform.position;
        }
    }
}