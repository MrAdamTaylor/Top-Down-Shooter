using System.Linq;
using Enemies.AI___NewArchitecture.Extensions;
using EnterpriceLogic.Constants;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float AttackCooldown = 2f;
    private float _attackCooldown;
    private float _effectDistance = 0.5f;
    private float _cleavege = 0.5f;
    private int _layerMask;

    private EnemyAnimator _animator;
    private Collider[] _hits = new Collider[1];
    private bool _isAttacking;
    private bool _attackIsActive;

    public void Construct(EnemyAnimator enemyAnimator)
    {
        _animator = enemyAnimator;
        _layerMask = 1 << LayerMask.NameToLayer("Player");
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
            PhysicsDebug.DrawDebugRaysFromPoint(HitPointPosition(), _cleavege, Constants.DEBUG_TIME_FRAMERATE);
        }
    }

    void OnAttackEnded()
    {
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
        return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward*_effectDistance;
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