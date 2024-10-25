using System;
using EnterpriceLogic.Constants;
using Logic.Animation;
using UnityEngine;

namespace Enemies
{
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int Attack1 = Animator.StringToHash("Attack_1");
        private static readonly int Attack2 = Animator.StringToHash("Attack_2");
        private static readonly int Attack3 = Animator.StringToHash("Attack_3");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Idle = Animator.StringToHash("ZombieIdle");

        private readonly int _idleStateHash = Animator.StringToHash("ZombieIdle");
        private readonly int _attackOneStateHash = Animator.StringToHash("ZombieAttack1");
        private readonly int _attackTwoStateHash = Animator.StringToHash("ZombieAttack2");
        private readonly int _attackThreeStateHash = Animator.StringToHash("ZombieAttack3");
        private readonly int _moveToHash = Animator.StringToHash("Move");
        private readonly int _deathToHash = Animator.StringToHash("DeathOfAZombie");
    
        private Animator _animator;

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }

        public void Construct()
        {
            _animator = GetComponent<Animator>();
            _animator.speed = Constants.ENEMY_ANIMATION_SPEED;
        }

        public void PlayIdle()
        {
            _animator.SetTrigger(Idle);
        }

        public void PlayDeath()
        {
            _animator.SetTrigger(Die);
        }

        public void Move(float speed)
        {
            _animator.SetBool(IsMoving, true);
            _animator.SetFloat(Speed, speed);
        }

        public void StopMoving()
        {
            _animator.SetBool(IsMoving, false);
        }

        public void PlayeAttack(int value)
        {
            switch (value)
            {
                case 1:
                    _animator.SetTrigger(Attack1);
                    break;
                case 2:
                    _animator.SetTrigger(Attack2);
                    break;
                case 3:
                    _animator.SetTrigger(Attack3);
                    break;
                default:
                    throw new Exception("Unknown Animator for Enemy State");
            }
        }

        public void ExitedState(int stateInfoShortNameHash)
        {
            StateExited?.Invoke(State);
        }

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
        
            if (stateHash == _idleStateHash)
                state = AnimatorState.Idle;
            else if (stateHash == _attackOneStateHash || stateHash == _attackTwoStateHash || stateHash == _attackThreeStateHash)
                state = AnimatorState.Attack;
            else if (stateHash == _moveToHash)
                state = AnimatorState.Walking;
            else if (stateHash == _deathToHash)
                state = AnimatorState.Died;
            else
                state = AnimatorState.Unknown;
            return state;
        }
    }
}