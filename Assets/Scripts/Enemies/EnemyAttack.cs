using System;
using System.Linq;
using Enemies.AI___NewArchitecture.Extensions;
using EnterpriceLogic.Constants;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAttack : MonoBehaviour
{
    public Action AfterAttackAction;
        
    public float AttackCooldown = Constants.ATTACK_COOLDOWN;
    private float _shiftedDistance = Constants.SHIFTED_DISTANCE;
    private float _cleavege = Constants.CLEAVE_RADIUS;
    private int _layerMask;
    private float _minDamage;
    private float _maxDamage;
    private EnemyAnimator _animator;

    private Collider[] _hits = new Collider[1];
    private float _attackCooldown;
    private bool _isAttacking;
    private bool _attackIsActive;

    public void Construct(EnemyAnimator enemyAnimator, float minDamage, float maxDamage)
    {
        _animator = enemyAnimator;
        _layerMask = 1 << LayerMask.NameToLayer("Player");
        _maxDamage = maxDamage;
        _minDamage = minDamage;
    }

    void Update()
    {
        UpdateCooldown();

        if(CanAttack())
            StartAttack();
    }

    void OnAttack()
    {
        if (Hit(out Collider hit))
        {
            PhysicsDebug.DrawDebugRaysFromPoint(HitPointPosition(), _cleavege, Constants.DEBUG_RILLRATE_TIME);
            PlayLoopComponentProvider provider = hit.transform.GetComponent<PlayLoopComponentProvider>();
            PlayerHealth health = (PlayerHealth)provider.TakeComponent(typeof(PlayerHealth));
            health.TakeDamage(Random.Range(_minDamage, _maxDamage));
        }
    }

    void OnAttackEnded()
    {
        AfterAttackAction?.Invoke();
        _attackCooldown = AttackCooldown;
        _isAttacking = false;
    }

    public void EnableAttack()
    {
        _attackIsActive = true;
    }

    public void DisableAttack()
    {
        _attackIsActive = false;
    }

    private bool Hit(out Collider hit)
    {
        var transformPosition = HitPointPosition();
        int hitCount = Physics.OverlapSphereNonAlloc(transformPosition, _cleavege, _hits, _layerMask);
        hit = _hits.FirstOrDefault();
        return hitCount > 0;
    }

    private Vector3 HitPointPosition()
    {
        return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward*_shiftedDistance;
    }

    private void UpdateCooldown()
    {
        if (!CooldownIsUp())
            _attackCooldown -= Time.deltaTime;
    }

    private bool CanAttack()
    {
        return _attackIsActive && !_isAttacking && CooldownIsUp();
    }

    private bool CooldownIsUp()
    {
        return _attackCooldown <= 0;
    }

    private void StartAttack()
    {
        _animator.PlayeAttack(Random.Range(Constants.MINIMAL_ATTACK_ANIMATION,Constants.MAXIMUM_ATTACK_ANIMATION));
        _isAttacking = true;
    }
}