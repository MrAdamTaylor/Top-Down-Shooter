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
            // Проверка на наличие инициализации камеры и точки стрельбы
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

            // Создаём луч в направлении курсора
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            // Определяем плоскость на уровне персонажа для получения направления
            Plane plane = new Plane(Vector3.up, _bulletPoint.position);
            if (plane.Raycast(ray, out float enter))
            {
                // Получаем направление от точки стрельбы к точке на плоскости
                Vector3 hitPoint = ray.GetPoint(enter);
                Vector3 direction = (hitPoint - _bulletPoint.position).normalized;

                // Выполняем Raycast в полученном направлении, чтобы проверить попадание
                if (Physics.Raycast(_bulletPoint.position, direction, out RaycastHit hit, _distance, _layerMask))
                {
                    // Если пуля попадает во врага
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer(Constants.ENEMY_LAYER))
                    {
                        PlayLoopComponentProvider enemyComponentProvider =
                            hit.collider.gameObject.GetComponent<PlayLoopComponentProvider>();
                        EnemyHealth enemyHealth = (EnemyHealth)enemyComponentProvider.TakeComponent(typeof(EnemyHealth));
                        enemyHealth.TakeDamage(_damage);
                    }

                    // Создаём пулю и эффект попадания в точке соприкосновения
                    _specialEffectFactory.CreateBullet(this, _bulletPoint.position, hit.point, hit.normal, _bulletSpeed, Constants.MADE_IMPACT);
                }
                else
                {
                    // Если пуля не попадает ни во что, летит на максимальное расстояние
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

