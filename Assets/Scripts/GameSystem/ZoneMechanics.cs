using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum ZoneWorkStage
{
    No,
    Warning,
    Make
}

public class ZoneMechanics : MonoBehaviour
{
    [Range(0.1f, 1f)][SerializeField] private float _speedChange; 
    
    [SerializeField] private Player _player;
    
    [SerializeField] private float _radius;

    [SerializeField] private CircleDrawer _drawer;

    public ZoneWorkStage Stage;
    [Range(0, 100)] 
    [SerializeField]private float _stageLevel;

    private void Awake()
    {
        Stage = ZoneWorkStage.No;
        _stageLevel = 0;
    }

    public void Update()
    {
        bool playerInZone = false;
        Collider[] targetsZone = Physics.OverlapSphere(transform.position, _radius);

        for (int i = 0; i < targetsZone.Length; i++)
        {
            //Debug.Log(targetsZone[i].gameObject.name);
            if (targetsZone[i].gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                playerInZone = true;
                break;
            }
        }
        UpdateZoneState(playerInZone);
    }

    private void UpdateZoneState(bool playerInZone)
    {
        switch (Stage)
        {
            case ZoneWorkStage.No:
                if (playerInZone)
                    Stage = ZoneWorkStage.Warning;
                break;
            case ZoneWorkStage.Warning:
                if (playerInZone)
                {
                    _stageLevel++;
                    if (_stageLevel >= 100)
                        Stage = ZoneWorkStage.Make;
                }
                else
                {
                    _stageLevel--;
                    if (_stageLevel <= 0)
                        Stage = ZoneWorkStage.No;
                }
                break;
            case ZoneWorkStage.Make:
                if (playerInZone)
                {
                    _player.SwitchSpeed(_speedChange);
                    //_player.Speed = (_player.Speed - (_player.Speed * 0.6f));
                }
                else
                {
                    _player.SwitchSpeed(Constants.STANDART_VALUE_FOR_SPEED);
                    Stage = ZoneWorkStage.Warning;
                }
                break;
        }
        
    }

    public void OnValidate()
    {
        if (_drawer != null)
        {
            _drawer.SetRadius(_radius);
            //_radius = _drawer.Radius;
            //Debug.Log(_radius);
        }
    }
}


