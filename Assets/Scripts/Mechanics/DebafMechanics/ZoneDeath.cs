using EnterpriceLogic.Constants;
using UnityEngine;

namespace Mechanics.DebafMechanics
{
    public class ZoneDeath : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private CircleDrawer _drawer;
        [Range(0, Constants.MAX_DEBUF_STAGE_LEVEL)] [SerializeField] private float _stageLevel;
        [SerializeField] private ParticleSystem _particleSystem;

        public ZoneWorkStage Stage;
        
        private Player _player;
        private Death _death;

        void Awake()
        {
            _particleSystem.Play();
            Stage = ZoneWorkStage.No;
            _stageLevel = 0;
        }

        void Start()
        {
            _player = (Player)ServiceLocator.Instance.GetData(typeof(Player));
        }

        void OnValidate()
        {
            if (_drawer != null)
            {
                _drawer.SetRadius(_radius);
            }
        }

        void Update()
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
                        if (_stageLevel >= Constants.MAX_DEBUF_STAGE_LEVEL)
                            Stage = ZoneWorkStage.Make;
                    }
                    else
                    {
                        _stageLevel--;
                        if (_stageLevel <= Constants.MIN_DEBUF_STAGE_LEVEL)
                            Stage = ZoneWorkStage.No;
                    }

                    break;
                case ZoneWorkStage.Make:
                    if (playerInZone)
                    {
                        _death = _player.gameObject.GetComponent<Death>();
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
    }
}