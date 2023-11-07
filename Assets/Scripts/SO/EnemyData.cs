using UnityEngine;

namespace Archero.SO
{
    [CreateAssetMenu]
    public class EnemyData : ScriptableObject
    {
        [SerializeField, Min(0.1f)] private float _speedMovement = 10;
        [SerializeField, Min(1)] private int _health = 100;
        [SerializeField, Min(0)] private float _timeOutStay = 2;
        [SerializeField, Min(0.1f)] private float _distanceMove = 1;
        [SerializeField, Min(0.2f)] private float _distanceToStartChase = 4;
        [SerializeField] private WeaponData _weapon;

        public float SpeedMovement => _speedMovement;
        public int Health => _health;

        public float DistanceMove => _distanceMove;

        public float DistanceToStartChase => _distanceToStartChase;

        public WeaponData Weapon => _weapon;
    }
}