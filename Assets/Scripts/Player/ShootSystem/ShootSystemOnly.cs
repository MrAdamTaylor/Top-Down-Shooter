using Enemies;
using EnterpriceLogic.Constants;
using EnterpriceLogic.Utilities;
using UnityEngine;

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