using UnityEngine;

namespace Mechanics.DebafMechanics
{
    public class ZonesSpawner : MonoBehaviour
    {
        [SerializeField] private ZoneDeath _zoneDeath;
        [SerializeField] private int _zoneDeathCount;
    
        [SerializeField] private ZoneTimeSlowed _slowed;
        [SerializeField] private int _zoneSlowedCount;

        [SerializeField] private ZoneSpawnerTrigger _trigger;
        [SerializeField] private Transform _position;

        [SerializeField] private Player _player;

        private float _maxDistance;
        
        void Awake()
        {
            _maxDistance = _trigger.MaxRadius;
        }

        void Start()
        {
            for (int i = 0; i < _zoneDeathCount; i++)
            {
                bool isEnough = false;
                Vector3 workVector = Vector3.zero;
                while (!isEnough)
                {
                    Vector3 vec = new Vector3(Random.Range(-_maxDistance, _maxDistance),
                        0, Random.Range(-_maxDistance, _maxDistance));
                    workVector = vec;
                    isEnough = _trigger.CheckPosition(vec);
                }
                ZoneDeath obj = Instantiate(_zoneDeath, workVector, Quaternion.identity);
                obj.gameObject.transform.parent = transform;
            }
            for (int i = 0; i < _zoneSlowedCount; i++)
            {
                bool isEnough = false;
                Vector3 workVector = Vector3.zero;
                while (!isEnough)
                {
                    Vector3 vec = new Vector3(Random.Range(-_maxDistance, _maxDistance),
                        0, Random.Range(-_maxDistance, _maxDistance));
                    workVector = vec;
                    isEnough = _trigger.CheckPosition(vec);
                }
                ZoneTimeSlowed obj = Instantiate(_slowed, workVector, Quaternion.identity);
                obj.gameObject.transform.parent = this.transform;
            }
        }
    }
}
