using UnityEngine;

namespace Mechanics.DebafMechanics
{
    public enum ZoneWorkStage
    {
        No,
        Warning,
        Make
    }

    public class ZoneTimeSlowed : MonoBehaviour
    {
        [Range(0.1f, 1f)][SerializeField] private float _speedChange;


        [SerializeField] private float _radius;

        [SerializeField] private CircleDrawer _drawer;

        [SerializeField] private ParticleSystem _particleSystem;

        [Range(0, 100)] 
        [SerializeField]private float _stageLevel;
        
        public ZoneWorkStage Stage;

        private Player _player;

        private void Awake()
        {
            _particleSystem.Play();
            Stage = ZoneWorkStage.No;
            _stageLevel = 0;
        }

        private void Start()
        {
            _player = (Player)ServiceLocator.Instance.GetData(typeof(Player));
        }

        private void OnValidate()
        {
            if (_drawer != null)
            {
                _drawer.SetRadius(_radius);
            }
        }

        private void Update()
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
                        _player.SwitchSpeed(_speedChange);
                    }
                    else
                    {
                        _player.SwitchSpeed(Constants.STANDART_VALUE_FOR_SPEED);
                        Stage = ZoneWorkStage.Warning;
                    }
                    break;
            }
        
        }
    }
}