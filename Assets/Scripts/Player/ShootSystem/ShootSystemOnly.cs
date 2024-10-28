using UnityEngine;
using Enemies;
using EnterpriceLogic.Constants;
using EnterpriceLogic.Utilities;
using Infrastructure.ServiceLocator;
using Infrastructure.Services.AbstractFactory;
using Logic;
using Weapon.StaticData;

namespace Player.ShootSystem
{
    public class ShootSystemOnly : CoomoonShootSystem
    {
        private float _bulletSpeed;
        private float _distance;

        private ISpecialEffectFactory _specialEffectFactory;
        private Transform _bulletPoint;
        private int _damage;
        private Camera _camera;

        public override void Construct(WeaponStaticData staticData, WeaponEffectsConteiner conteiner)
        {
            _layerMask = Constants.WEAPON_LAYER_MASK;
            _directionObject = (Transform)ServiceLocator.Instance.GetData(typeof(Transform));
            _directionObject.IsNullWithException("Transform not constructed in ShootSystemOnly");
            _bulletSpeed = Constants.DEFAULT_BULLET_SPEED;
            _distance = Constants.DEFAULT_BULLET_DISTANCE;
            _damage = staticData.Damage;
            _bulletPoint = staticData.BulletPoint;
            _specialEffectFactory = (ISpecialEffectFactory)ServiceLocator.Instance.GetData(typeof(ISpecialEffectFactory));

            // Автоматически находим камеру
            _camera = Camera.main;
            if (_camera == null)
            {
                Debug.LogError("Main camera not found. Please ensure the camera has the 'MainCamera' tag.");
            }
        }


        public override void Shoot()
        {
            if (_camera == null)
            {
                Debug.LogError("Camera not initialized in ShootSystemOnly. Please ensure Construct is called.");
                return;
            }

            if (_bulletPoint == null)
            {
                Debug.LogError("Bullet point not set in ShootSystemOnly. Ensure WeaponStaticData has BulletPoint assigned.");
                return;
            }

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, _bulletPoint.position);

            if (plane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                Vector3 direction = (hitPoint - _bulletPoint.position).normalized;

                // Устанавливаем минимальную дистанцию
                float minDistance = 2.0f; // Можно регулировать по необходимости
                if (Vector3.Distance(_bulletPoint.position, hitPoint) < minDistance)
                {
                    // Если курсор слишком близко, стреляем прямо вперед от игрока
                    direction = _bulletPoint.forward; // Вектор вперед относительно точки выстрела
                }

                if (Physics.Raycast(_bulletPoint.position, direction, out RaycastHit hit, _distance, _layerMask))
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer(Constants.ENEMY_LAYER))
                    {
                        PlayLoopComponentProvider enemyComponentProvider =
                            hit.collider.gameObject.GetComponent<PlayLoopComponentProvider>();
                        EnemyHealth enemyHealth = (EnemyHealth)enemyComponentProvider.TakeComponent(typeof(EnemyHealth));
                        enemyHealth.TakeDamage(_damage);
                    }

                    _specialEffectFactory.CreateBullet(this, _bulletPoint.position, hit.point, hit.normal, _bulletSpeed, Constants.MADE_IMPACT);
                }
                else
                {
                    _specialEffectFactory.CreateBullet(this, _bulletPoint.position, _bulletPoint.position + direction * _distance, Vector3.zero, _bulletSpeed, Constants.NON_MADE_IMPACT);
                }
            }
        }




    }

    /* namespace Player.ShootSystem
     {
         public class ShootSystemOnly : CoomoonShootSystem
         {
             private float _bulletSpeed;
             private float _distance;

             private ISpecialEffectFactory _specialEffectFactory;
             private Transform _bulletPoint;
             private int _damage;

             public override void Construct(WeaponStaticData staticData, WeaponEffectsConteiner conteiner)
             {
                 _layerMask = Constants.WEAPON_LAYER_MASK;
                 _directionObject = (Transform)ServiceLocator.Instance.GetData(typeof(Transform));
                 _directionObject.IsNullWithException("Transform not constructed in ShootSystemOnly");
                 _bulletSpeed = Constants.DEFAULT_BULLET_SPEED;
                 _distance = Constants.DEFAULT_BULLET_DISTANCE;
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
                 Vector3 direction = GetDirection();
                 if (Physics.Raycast(_bulletPoint.position, direction, out RaycastHit hit, _distance, _layerMask))
                 {
                     if (hit.collider.gameObject.layer == LayerMask.NameToLayer(Constants.ENEMY_LAYER))
                     {

                         PlayLoopComponentProvider enemyComponentProvider =
                             hit.collider.gameObject.GetComponent<PlayLoopComponentProvider>();
                         EnemyHealth enemyHealth = (EnemyHealth)enemyComponentProvider.TakeComponent(typeof(EnemyHealth));
                         enemyHealth.TakeDamage(_damage);
                     }

                     _specialEffectFactory.CreateBullet(this, _bulletPoint.position, hit.point, hit.normal, _bulletSpeed, Constants.MADE_IMPACT);
                 }
                 else
                 {
                     _specialEffectFactory.CreateBullet(this, _bulletPoint.position,
                         _bulletPoint.position + GetDirection() * _distance, Vector3.zero, _bulletSpeed, Constants.NON_MADE_IMPACT);
                 }
             }
         }
     }
    */

}

