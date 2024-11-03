using UnityEngine;

namespace Enemies.EnemyStateMachine
{
    public class NPCStateMachine : MonoBehaviour
    {
        public const string NO_STATE = "(no current state)";
        private BaseState _currentState;

        public void Start()
        {
            _currentState = GetDefaultState();
            if(_currentState != null)
                _currentState.Enter();
        }

        void Update()
        {
            if(_currentState!= null)
                _currentState.UpdateLogic();
        }

        void LateUpdate()
        {
            if(_currentState!=null)
                _currentState.UpdatePhysics();
        }

        public void ChangeState(BaseState newState)
        {
            _currentState.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
    
        protected virtual BaseState GetDefaultState()
        {
            return null;
        }

        private void OnGUI()
        {
            string content = _currentState != null ? _currentState.Name : NO_STATE;
            GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
        }
    }
}