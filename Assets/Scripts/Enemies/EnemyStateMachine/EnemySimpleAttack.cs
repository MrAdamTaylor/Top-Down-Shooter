using System;
using System.Linq;
using EnterpriceLogic;
using EnterpriceLogic.Constants;
using Logic;
using Logic.Animation;
using Player;
using UnityEngine;

namespace Enemies.EnemyStateMachine
{
    public class EnemySimpleAttack : MonoBehaviour, IEnemyAttack
    {
        private const string ANIMATION_ATACK_START = "Attack_Start";
        private const string ANIMATION_ATACK_END = "Attack_End";
    
        private const float ATTACK_COOLDOWN = 1f;
        private const float SHIFTED_DISTANCE = 0.5f;
        private const float CLEAVE_RADIUS = 0.8f;
        private const float HIT_BOX_HIGH_SHIFTED = 0.6f;

        private int _layerMask;
        private float _minDamage;
        private float _maxDamage;
        private EnemyAnimator _animator;
        private EnemyAnimationEvent _animationEvent;
        private Collider[] _hits = new Collider[1];
        private float _attackCooldown;

        public void Construct(EnemyAnimator enemyAnimator, float minDamage, float maxDamage)
        {
            _animator = enemyAnimator;
            _layerMask = 1 << LayerMask.NameToLayer("Player");
            _maxDamage = maxDamage;
            _minDamage = minDamage;
            _animationEvent = transform.GetComponent<EnemyAnimationEvent>();
            _animationEvent.EnemyAnimEvent.AddListener(OnAnimationEvent);
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

        private void AttackEnd()
        {
            _attackCooldown = ATTACK_COOLDOWN;
        }
        
        private bool Hit(out Collider hit)
        {
            var transformPosition = HitPointPosition();
            int hitCount = Physics.OverlapSphereNonAlloc(transformPosition, CLEAVE_RADIUS, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            return hitCount > 0;
        }

        private void Attack()
        {
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebugRaysFromPoint(HitPointPosition(), CLEAVE_RADIUS, Constants.DEBUG_RILLRATE_TIME);
                PlayLoopComponentProvider provider = hit.transform.GetComponent<PlayLoopComponentProvider>();
                PlayerHealth health = (PlayerHealth)provider.TakeComponent(typeof(PlayerHealth));
                //health.TakeDamage(Random.Range(_minDamage, _maxDamage));
            }
        }
        
        private Vector3 HitPointPosition()
        {
            return new Vector3(transform.position.x, transform.position.y + HIT_BOX_HIGH_SHIFTED, transform.position.z) + transform.forward*SHIFTED_DISTANCE;
        }
    }
}