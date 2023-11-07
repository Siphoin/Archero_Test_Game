using UnityEngine;

namespace Archero
{
    public class SpawnPoint : MonoBehaviour, ISpawnPoint
    {
        [SerializeField] private SpawnPointType _type;

        public Vector3 Position => transform.position;

        public SpawnPointType Type => _type;
    }
}