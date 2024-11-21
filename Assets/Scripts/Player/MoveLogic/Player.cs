using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Player.MoveLogic
{
    public class Player 
    {
        
    }
    
    public class PlayerComponentController : MonoBehaviour
    {
        private List<PlayerComponent> _playerComponents = new();
        
        [Inject]
        public void Construct()
        {
            
        }

        private void Update()
        {
            for (int i = 0; i < _playerComponents.Count; i++)
            {
                _playerComponents[i].Tick();
            }
        }
    }

    public class PlayerComponent
    {
        public void Tick() => OnTick();

        private void OnTick() { }
    }
}
