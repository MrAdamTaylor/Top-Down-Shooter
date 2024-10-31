using System.Collections.Generic;
using Enemies;
using EnterpriceLogic.Constants;
using EnterpriceLogic.Utilities;
using Infrastructure.ServiceLocator;
using Infrastructure.Services.AbstractFactory;
using Logic;
using UnityEngine;
using Weapon;
using Weapon.StaticData;

namespace Player.ShootSystem
{
    public class ShootSystemFraction : CoomoonShootSystem
    {
        private const int THIRD_FRACTION = 3;
        private const int FIVE_FRACTION = 5;
        
        private float _distance;
        private LineRenderer _laser;
        private float _fadeDuration = 0.3f;
        private GameObject _shootTrash;
        private Camera _camera;

        [SerializeField] private FractionShotCharacteristics _fractionShotCharacteristics;

        private List<Vector3> _directions;
        private Transform _bulletPoint;
        private int _damage;
        private ISpecialEffectFactory _specialEffectFactory;

        public override void Construct(WeaponStaticData staticData, WeaponEffectsConteiner conteiner)
        {
            WeaponStaticShootgun staticShootgun = (WeaponStaticShootgun)staticData;
            _layerMask = Constants.WEAPON_LAYER_MASK;
            _directionObject = (Transform)ServiceLocator.Instance.GetData(typeof(Transform));
            _directionObject.IsNullWithException("Transform not constructed in ShootSystemOnly");
            _laser = conteiner.GetLineRenderer();
            _fractionShotCharacteristics.AmountFraction = staticShootgun.AmountOfFraction;
            _fractionShotCharacteristics.Angle = staticShootgun.AmountOfFraction;
            _fractionShotCharacteristics.Distance = staticShootgun.Distance;
            _fadeDuration = staticShootgun.FadeDuration;
            _shootTrash = new GameObject(ConstantsSceneObjects.FRACTION_TRASH);
            _shootTrash.transform.SetParent(transform);
            _damage = staticData.Damage;
            _bulletPoint = staticData.BulletPoint;
            _specialEffectFactory = (ISpecialEffectFactory)ServiceLocator.Instance.GetData(typeof(ISpecialEffectFactory));
        }

        public override void UpdateValues(WeaponCharacteristics characteristics)
        {
            _damage = characteristics.Damage;
        }



        public override void Shoot()
        {
            if (_fractionShotCharacteristics.Distance != 0)
                _distance = _fractionShotCharacteristics.Distance;
            
            Vector3 baseDirection = GetMouseDirection();
            if (baseDirection == Vector3.zero)
            {
                Debug.LogWarning("Direction to cursor not calculated correctly.");
                return;
            }
            
            switch (_fractionShotCharacteristics.AmountFraction)
            {
                case THIRD_FRACTION:
                    ThirdFractionShoot(baseDirection);
                    break;
                case FIVE_FRACTION:
                    FiveFractionShoot(baseDirection);
                    break;
            }
        }

        private Vector3 GetMouseDirection()
        {
            if (_camera == null)
            {
                _camera = Camera.main;
                if (_camera == null)
                {
                    Debug.LogError("Camera not found in ShootSystemFraction.");
                    return Vector3.forward;
                }
            }

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, _bulletPoint.position);

            if (plane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                Vector3 direction = (hitPoint - _bulletPoint.position).normalized;
                
                if (Vector3.Distance(hitPoint, _bulletPoint.position) < 2f)
                {
                    return _bulletPoint.forward;
                }

                return direction;
            }
            
            return _bulletPoint.forward;
        }


        private void FiveFractionShoot(Vector3 baseDirection)
        {
            _directions = new List<Vector3> { baseDirection };
            
            float angleOffset = _fractionShotCharacteristics.Angle / 2; // �������� ���� ��� ������������ �������������
            
            for (int i = 1; i <= 2; i++)
            {
                float angle = angleOffset * i;
                _directions.Add(Quaternion.AngleAxis(angle, Vector3.up) * baseDirection);  // ������
                _directions.Add(Quaternion.AngleAxis(-angle, Vector3.up) * baseDirection); // �����
            }

            ShootFraction();
        }

        private void ThirdFractionShoot(Vector3 baseDirection)
        {
            _directions = new List<Vector3> { baseDirection };
            
            float angleOffset = _fractionShotCharacteristics.Angle / 2;
            
            _directions.Add(Quaternion.AngleAxis(angleOffset, Vector3.up) * baseDirection);
            _directions.Add(Quaternion.AngleAxis(-angleOffset, Vector3.up) * baseDirection);

            ShootFraction();
        }

        private void ShootFraction()
        {
            if (_shootTrash.transform.childCount != 0)
            {
                foreach (Transform child in _shootTrash.transform)
                {
                    Destroy(child.gameObject);
                }
            }

            for (int i = 0; i < _directions.Count; i++)
            {
                if (Physics.Raycast(_bulletPoint.position, _directions[i], out RaycastHit hit, _distance, _layerMask))
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer(Constants.ENEMY_LAYER))
                    {
                        PlayLoopComponentProvider enemyComponentProvider =
                            hit.collider.gameObject.GetComponent<PlayLoopComponentProvider>();
                        EnemyHealth enemyHealth = (EnemyHealth)enemyComponentProvider.TakeComponent(typeof(EnemyHealth));
                        enemyHealth.TakeDamage(_damage);
                    }
                    _specialEffectFactory.CreateLaser(this, _laser, _bulletPoint.position, hit.point, _fadeDuration, _shootTrash.transform);
                }
                else
                {
                    _specialEffectFactory.CreateLaser(this, _laser, _bulletPoint.position, _bulletPoint.position + _directions[i] * _distance, _fadeDuration, _shootTrash.transform);
                }
            }
        }

    }
    

    [System.Serializable]
    public struct FractionShotCharacteristics
    {
        public int AmountFraction;
        public float Angle;
        public float Distance;
    }
}