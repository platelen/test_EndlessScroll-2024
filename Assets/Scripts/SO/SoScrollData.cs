using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "ScrollData")]
    public class SoScrollData:ScriptableObject
    {
        [SerializeField] private float _startSpeed = 100f;
        [SerializeField] private float _maxSpeed = 1000f;
        [SerializeField] private float _accelerationTime = 4f;

        public float StartSpeed => _startSpeed;

        public float MaxSpeed => _maxSpeed;

        public float AccelerationTime => _accelerationTime;
    }
}