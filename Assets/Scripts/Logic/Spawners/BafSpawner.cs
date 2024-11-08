using Configs;
using EnterpriceLogic;
using Infrastructure.ServiceLocator;
using Infrastructure.Services.AbstractFactory;
using Logic.Timer;
using UnityEngine;

namespace Logic.Spawners
{
    public class BafSpawner : MonoBehaviour
    {
        private const int MAX_COLLIDERS_COUNT = 7;
        
        private const float XCenterPosition = 4f;
        private const float YCenterPosition = 1f;
        private const float ZCenterPosition = -3f;

        private const float ZAxisSize = 53f;
        private const float XAxisSize = 62f;

        private IBafFactory _bafFactory;
        private BafSpawnerConfigs _bafSpawnerConfigs;
        private RingTrigger _ringTrigger;
        private Timer.Timer _timer;

        private Vector3 _center;
        private Vector3 _regizonSize;
        
        private bool _isConstucted;

        #region Borders

        private float _rightBorder;
        private float _leftBorder;
        private float _topBorder;
        private float _downBorder;
        private float _innerSpawnInterval;

        #endregion
        private Transform _playerTransform;
        private Collider[] _hitColliders;
        public void Construct(IBafFactory bafFactory, BafSpawnerConfigs bafSpawnerConfigs, RingTrigger ringTrigger)
        {
            _timer = new Timer.Timer(TimerType.OneSecTick, bafSpawnerConfigs.FirstTimeWait);
            _timer.OnTimerFinishEvent += SpawnObject;
            _timer.Start();
            _innerSpawnInterval = bafSpawnerConfigs.SpawnInterval;
            _ringTrigger = ringTrigger;
            _bafFactory = bafFactory;
            _bafSpawnerConfigs = bafSpawnerConfigs;
            Player.Player player = (Player.Player)Infrastructure.ServiceLocator.ServiceLocator.Instance.GetData(typeof(Player.Player));
            _playerTransform = player.transform;

            _center = new Vector3(XCenterPosition, YCenterPosition, ZCenterPosition);
            _regizonSize = new Vector3(XAxisSize, 0f, ZAxisSize);
            _isConstucted = true;

            _rightBorder = XCenterPosition + XAxisSize/2;
            _leftBorder = XCenterPosition - XAxisSize/2;
            _topBorder = ZCenterPosition + ZAxisSize/2;
            _downBorder = ZCenterPosition - ZAxisSize/2;

            _hitColliders = new Collider[MAX_COLLIDERS_COUNT];
            ServiceLocator.Instance.BindData(typeof(BafSpawner), this);
        }

        private void OnDrawGizmos()
        {
            if (_isConstucted)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawWireCube(_center, _regizonSize);
            }
        }

        private void SpawnObject()
        {
            Vector3 finalPosition = CalculateSpawnPosition();

            int index = Random.Range(0, _bafSpawnerConfigs.BafConfigsList.Count);
            _bafFactory.Create(_bafSpawnerConfigs.BafConfigsList[index], finalPosition);
            if (_bafSpawnerConfigs.IncreaseSpawnTime)
            {
                _innerSpawnInterval += _bafSpawnerConfigs.PerSecTimeIncrease;
                _timer.SetTime(_innerSpawnInterval);
            }
            else
            {
                _timer.SetTime(_bafSpawnerConfigs.SpawnInterval);
            }
            _timer.Start();
        }

        private Vector3 CalculateSpawnPosition()
        {
            float xValuePositive = _playerTransform.position.x + _ringTrigger.MaxRadiusAbs;
            float zValuePozitive = _playerTransform.position.z + _ringTrigger.MaxRadiusAbs;
                
            float xValueNegative = _playerTransform.position.x - _ringTrigger.MaxRadiusAbs;
            float zValueNegative = _playerTransform.position.z - _ringTrigger.MaxRadiusAbs;

            bool isRightSpawn = false;

            Vector3 finalPosition = new Vector3(0,0,0);
            while (!isRightSpawn)
            {
                float randomXCord = Random.Range(xValueNegative, xValuePositive);
                float randomZCord = Random.Range(zValueNegative, zValuePozitive);
                finalPosition = new Vector3(randomXCord,1.5f,randomZCord);

                bool isRinging = _ringTrigger.IsInRing(finalPosition);
                bool isNoCollide = CheckCollide(finalPosition);
                bool isNoBordered = CheckBordersMap(finalPosition);
                isRightSpawn = isRinging && isNoCollide && isNoBordered;
            }

            return finalPosition;
        }

        private bool CheckBordersMap(Vector3 finalPosition)
        {
            bool isLeft = finalPosition.x > _leftBorder;
            bool isRight = finalPosition.x < _rightBorder;

            bool isTop = finalPosition.z < _topBorder;
            bool isDown = finalPosition.z > _downBorder;

            return isLeft && isRight && isTop && isDown;
        }

        private bool CheckCollide(Vector3 finalPosition)
        {
            int numColliders = Physics.OverlapSphereNonAlloc(finalPosition, 1f, _hitColliders);
            return numColliders <= 0;
        }
    }
}