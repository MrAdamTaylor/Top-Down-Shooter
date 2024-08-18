using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

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
    private Vector3 _center;
    
    
    private void Awake()
    {
        _center = _position.position;
        _maxDistance = _trigger.MaxRadius;
    }

    private void Start()
    {
        for (int i = 0; i < _zoneDeathCount; i++)
        {
            bool isEnough = false;
            Vector3 workVector = new Vector3(0f,0f,0f);
            while (!isEnough)
            {
                Vector3 vec = new Vector3(UnityEngine.Random.Range(-_maxDistance, _maxDistance),
                    0, UnityEngine.Random.Range(-_maxDistance, _maxDistance));
                workVector = vec;
                isEnough = _trigger.CheckPosition(vec);
            }
            ZoneDeath obj = Instantiate(_zoneDeath, workVector, Quaternion.identity);
            obj.gameObject.transform.parent = this.transform;
            obj.Player = _player;
        }
        for (int i = 0; i < _zoneSlowedCount; i++)
        {
            bool isEnough = false;
            Vector3 workVector = new Vector3(0f,0f,0f);
            while (!isEnough)
            {
                Vector3 vec = new Vector3(UnityEngine.Random.Range(-_maxDistance, _maxDistance),
                    0, UnityEngine.Random.Range(-_maxDistance, _maxDistance));
                workVector = vec;
                isEnough = _trigger.CheckPosition(vec);
            }
            ZoneTimeSlowed obj = Instantiate(_slowed, workVector, Quaternion.identity);
            obj.gameObject.transform.parent = this.transform;
            obj.Player = _player;
        }
    }
}
