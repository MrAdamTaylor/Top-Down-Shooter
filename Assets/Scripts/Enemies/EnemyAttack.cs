using System;
using System.Linq;
using Enemies.EnemyStateMachine;
using EnterpriceLogic;
using EnterpriceLogic.Constants;
using Logic;
using Logic.Animation;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class EnemyAttack : MonoBehaviour, IEnemyAttack
    {
        private const string ANIMATION_ATACK_START = "Attack_Start";
        private const string ANIMATION_ATACK_END = "Attack_End";
    
        private const float ATTACK_COOLDOWN = 1f;
        private const float SHIFTED_DISTANCE = 0.5f;
        private const float CLEAVE_RADIUS = 0.8f;
        private const float HIT_BOX_HIGH_SHIFTED = 0.6f;
    
        public Action<bool> ReadyForAction;
    
        private int _layerMask;
        private float _minDamage;
        private float _maxDamage;
        private EnemyAnimator _animator;

        private Collider[] _hits = new Collider[1];
        private float _attackCooldown;
        private bool _isAttacking;
        private bool _attackIsActive;
        private EnemyAnimationEvent _animationEvent;
        private bool _isActual;
        private Action _attackEnd;

        Action IEnemyAttack.ActionAttackEnd
        {
            get => _attackEnd;
            set => _attackEnd = value;
        }

        public bool IsCanAttack { get; private set; }

        public void Construct(EnemyAnimator enemyAnimator, float minDamage, float maxDamage)
        {
            _animator = enemyAnimator;
            _layerMask = 1 << LayerMask.NameToLayer("Player");
            _maxDamage = maxDamage;
            _minDamage = minDamage;
            _animationEvent = transform.GetComponent<EnemyAnimationEvent>();
            _animationEvent.EnemyAnimEvent.AddListener(OnAnimationEvent);
            _isActual = true;
        }

        void IEnemyAttack.Attack()
        {
            Attack();
        }

        private void Update()
        {
            UpdateCooldown();

            if(CanAttack())
                StartAttack();
        }

        public void Reset()
        {
            _attackIsActive = false;
            _isAttacking = false;
            _attackCooldown = ATTACK_COOLDOWN;
        }

        private void OnDestroy()
        {
            _animationEvent.EnemyAnimEvent.RemoveListener(OnAnimationEvent);
        }

        public void EnableAttack()
        {
            _attackIsActive = true;
        }


        public void DisableAttack()
        {
            _attackIsActive = false;
        }

        public void ActualAttacking(bool actual)
        {
            _isActual = actual;
        }

        private void OnAnimationEvent(string eventName)
        {
            Debug.Log($"<color=yellow>Animation Event: {eventName}</color>");
            switch (eventName)
            {
                case ANIMATION_ATACK_START:
                    Attack();
                    break;
                case ANIMATION_ATACK_END:
                    AttackEnd();
                    break;
                default:
                    throw new Exception("Unknown Animation Type");
            }
        }

        private void Attack()
        {
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebugRaysFromPoint(HitPointPosition(), CLEAVE_RADIUS, Constants.DEBUG_RILLRATE_TIME);
                PlayLoopComponentProvider provider = hit.transform.GetComponent<PlayLoopComponentProvider>();
                PlayerHealth health = (PlayerHealth)provider.TakeComponent(typeof(PlayerHealth));
                health.TakeDamage(Random.Range(_minDamage, _maxDamage));
            }
        }

        private void AttackEnd()
        {
            ReadyForAction.Invoke(true);
            _attackCooldown = ATTACK_COOLDOWN;
            _isAttacking = false;
        }

        private bool Hit(out Collider hit)
        {
            var transformPosition = HitPointPosition();
            int hitCount = Physics.OverlapSphereNonAlloc(transformPosition, CLEAVE_RADIUS, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            return hitCount > 0;
        }

        private Vector3 HitPointPosition()
        {
            return new Vector3(transform.position.x, transform.position.y + HIT_BOX_HIGH_SHIFTED, transform.position.z) + transform.forward*SHIFTED_DISTANCE;
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _attackCooldown -= Time.deltaTime;
        }

        private bool CanAttack()
        {
            return _attackIsActive && !_isAttacking && CooldownIsUp() && _isActual;
        }

        public bool CooldownIsUp()
        {
            return _attackCooldown <= 0;
        }

        private void StartAttack()
        {
            ReadyForAction?.Invoke(false);
            _animator.PlayAttack(Random.Range(Constants.MINIMAL_ATTACK_ANIMATION,Constants.MAXIMUM_ATTACK_ANIMATION));
            _isAttacking = true;
        }
    }
}