using System;
using UnityEngine;

namespace Mechanics
{
    public class EnemyDeath : MonoBehaviour
    {
        public event Action OnDeath;
    
        public void MakeDeath()
        {
            OnDeath?.Invoke();
        }
    }
}