using System;
using Enemies;
using Infrastructure.ServiceLocator;
using Logic.Animation;
using UnityEngine;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour, IAnimationStateReader
    {
        public AnimatorState State { get; private set; }
        
        public event Action<AnimatorState> StateEntered;
        
        public event Action<AnimatorState> StateExited;

        #region Transition Naming
        private  static readonly int DeathTrigger = Animator.StringToHash("deathTrigger");
        private static readonly int XAxis = Animator.StringToHash("InputX");
        private  static readonly int YAxis = Animator.StringToHash("InputY");
        private  static readonly int IsAlive = Animator.StringToHash("IsAlive");
        #endregion
        
        #region State Naming
        private readonly int _deathState = Animator.StringToHash("Death");
        private readonly int _idleState = Animator.StringToHash("Locomotion");
        #endregion
        private static readonly int IsBigGun = Animator.StringToHash("isBigGun");
        private static readonly int IsSmallGun = Animator.StringToHash("isSmallGun");



        private Animator _animator;
        
        private Vector2 _input;
        public float smoothBlend = 0.1f;
        
        public void Construct()
        {
            _animator = GetComponent<Animator>();
            ServiceLocator.Instance.BindData(typeof(PlayerAnimator), this);
            _animator.SetBool(_idleState, true);
        }

        private void Update()
        {
            if(State == AnimatorState.Idle)
                Move(_input.x = Input.GetAxis("Horizontal"), _input.y = Input.GetAxis("Vertical"));
        }

        private void Move(float x, float y)
        {
            Vector3 direction = new Vector3(x, 0, y);
            direction = transform.InverseTransformDirection(direction);

            _animator.SetFloat(XAxis, direction.x, smoothBlend, Time.deltaTime);
            _animator.SetFloat(YAxis, direction.z, smoothBlend, Time.deltaTime);
        }

        public void SetWeaponType(bool isBigGun)
        {
           _animator.SetBool(IsBigGun, isBigGun);
            _animator.SetBool(IsSmallGun, !isBigGun);
            Debug.Log(IsSmallGun);
        }
        public void PlayIdle()
        {
            State = AnimatorState.Idle;
            _animator.SetTrigger(IsAlive);
            _animator.SetBool(_idleState, true);
            
        }

        public void PlayDeath()
        {
            //_animator.SetBool(_idleState, true);
            _animator.SetBool(_idleState, false);
            State = AnimatorState.Died;
            _animator.SetTrigger(DeathTrigger);
            //_animator.SetFloat(Speed, speed);
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