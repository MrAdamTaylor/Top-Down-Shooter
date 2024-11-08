using System;
using System.Linq;
using EnterpriceLogic.Constants;
using Logic;
using Logic.Animation;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies.EnemyStateMachine.Components
{
    public class EnemySimpleAttack : MonoBehaviour, IEnemyAttack
    {
        private const string ANIMATION_ATACK_START = "Attack_Start";
        private const string ANIMATION_ATACK_END = "Attack_End";
    
        private const float ATTACK_COOLDOWN = 1f;
        private const float SHIFTED_DISTANCE = 0.5f;
        private const float CLEAVE_RADIUS = 1.7f;
        private const float HIT_BOX_HIGH_SHIFTED = 0.6f;

        public Action ActionAttackEnd { get; set; }
    

        public bool IsCanAttack { get; private set; }

        private int _layerMask;
        private float _minDamage;
        private float _maxDamage;
        private EnemyAnimator _animator;
        private EnemyAnimationEvent _animationEvent;
        private Collider[] _hits = new Collider[1];
        private float _attackCooldown;
        private float _hitBoxRadius;
        
        private bool _isAttacking;
        private bool _attackIsPosible;

        public void Construct(EnemyAnimator enemyAnimator, float minDamage, float maxDamage, float hitBoxRadius)
        {
            _animator = enemyAnimator;
            _layerMask = 1 << LayerMask.NameToLayer("Player");
            _maxDamage = maxDamage;
            _minDamage = minDamage;
            _animationEvent = transform.GetComponent<EnemyAnimationEvent>();
            _animationEvent.EnemyAnimEvent.AddListener(OnAnimationEvent);
            _hitBoxRadius = hitBoxRadius;
        }

        void IEnemyAttack.Attack()
        {
            _animator.PlayAttack(Random.Range(Constants.MINIMAL_ATTACK_ANIMATION,Constants.MAXIMUM_ATTACK_ANIMATION));
            _isAttacking = true;
        }

        private void Update()
        {
            UpdateCooldown();
            IsCanAttack = _attackIsPosible && !_isAttacking && CooldownIsUp();
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _attackCooldown -= Time.deltaTime;
        }

        public void DisableAttack()
        {
            _attackIsPosible = false;
        }

        public void EnableAttack()
        {
            _attackIsPosible = true;
        }

        private void OnAnimationEvent(string eventName)
        {
            //Debug.Log($"<color=yellow>Animation Event: {eventName}</color>");
            switch (eventName)
            {
                case ANIMATION_ATACK_START:
                    MakeHit();
                    break;
                case ANIMATION_ATACK_END:
                    AttackEnd();
                    break;
                default:
                    throw new Exception("Unknown Animation Type");
            }
        }

        private void AttackEnd()
        {
            _isAttacking = false;
            _attackCooldown = ATTACK_COOLDOWN;
            ActionAttackEnd?.Invoke();
        }

        private bool Hit(out Collider hit)
        {
            var transformPosition = HitPointPosition();
            int hitCount = Physics.OverlapSphereNonAlloc(transformPosition, _hitBoxRadius, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            return hitCount > 0;
        }

        private void MakeHit()
        {
            if (Hit(out Collider hit))
            {
                //PhysicsDebug.DrawDebugRaysFromPoint(HitPointPosition(), _hitBoxRadius, Constants.DEBUG_RILLRATE_TIME);
                PlayLoopComponentProvider provider = hit.transform.GetComponent<PlayLoopComponentProvider>();
                PlayerHealth health = (PlayerHealth)provider.TakeComponent(typeof(PlayerHealth));
                health.TakeDamage(Random.Range(_minDamage, _maxDamage));
            }
        }

        private Vector3 HitPointPosition()
        {
            return new Vector3(transform.position.x, transform.position.y + HIT_BOX_HIGH_SHIFTED, transform.position.z) + transform.forward*SHIFTED_DISTANCE;
        }

        public bool CooldownIsUp()
        {
            return _attackCooldown <= 0f;
        }
    }
}