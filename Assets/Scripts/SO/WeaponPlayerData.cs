using UnityEngine;

namespace Archero.SO
{
    [CreateAssetMenu]
    public partial class WeaponPlayerData : WeaponData
    {
        [SerializeField, Min(0.1F)] private float _radius = 2;

        public float Radius => _radius;
    }
}