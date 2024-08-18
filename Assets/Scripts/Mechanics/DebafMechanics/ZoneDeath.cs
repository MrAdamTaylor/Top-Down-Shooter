using UnityEngine;
using UnityEngine.Serialization;

public class ZoneDeath : MonoBehaviour
{
    [Range(0.1f, 1f)] [SerializeField] private float _speedChange;

    public Player Player;

    [SerializeField] private float _radius;

    [SerializeField] private CircleDrawer _drawer;

    public ZoneWorkStage Stage;
    [Range(0, 100)] [SerializeField] private float _stageLevel;
    
    [SerializeField] private ParticleSystem _particleSystem;

    private Death _death;

    private void Awake()
    {
        _particleSystem.Play();
        Stage = ZoneWorkStage.No;
        _stageLevel = 0;
    }

    public void Update()
    {
        bool playerInZone = false;
        Collider[] targetsZone = Physics.OverlapSphere(transform.position, _radius);

        for (int i = 0; i < targetsZone.Length; i++)
        {
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
                    _death = Player.gameObject.GetComponent<Death>();
                    _death.MakeDeath();
                }
                else
                {
                    _death = null;
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
        }
    }
}