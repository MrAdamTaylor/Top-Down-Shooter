using System;
using Enemies;
using Logic.Animation;
using UnityEngine;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour, IAnimationStateReader
    {
        public AnimatorState State { get; private set; }
        
        public event Action<AnimatorState> StateEntered;
        
        public event Action<AnimatorState> StateExited;

        private static readonly int DeathTrigger = Animator.StringToHash("deathTrigger");

        private  readonly int _deathState = Animator.StringToHash("Death");
        private  readonly int _idleState = Animator.StringToHash("Locomotion");
        private Animator _animator;
        
        public void Construct()
        {
            _animator = GetComponent<Animator>();
        }
        
        public void PlayIdle()
        {
            State = AnimatorState.Idle;
            _animator.SetTrigger(_deathState);
        }

        public void PlayDeath()
        {
            _animator.SetTrigger(DeathTrigger);
        }
        
        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateInfoShortNameHash)
        {
            StateExited?.Invoke(State);
        }
        
        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
        
            if (stateHash == _idleState)
                state = AnimatorState.Idle;
            else if (stateHash == _deathState)
                state = AnimatorState.Died;
            else
                state = AnimatorState.Unknown;
            return state;
        }

    }
}