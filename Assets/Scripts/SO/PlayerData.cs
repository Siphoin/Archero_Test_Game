using UnityEngine;

namespace Archero.SO
{
    [CreateAssetMenu]
    public class PlayerData : ScriptableObject
    {
        [SerializeField, Min(0.1f)] private float _speedMovement = 10;
        [SerializeField, Min(1)] private int _health = 100;

        public float SpeedMovement => _speedMovement;
        public int Health => _health;
    }
}