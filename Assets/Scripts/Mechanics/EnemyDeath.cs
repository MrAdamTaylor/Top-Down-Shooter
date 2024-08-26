using System;
using Mechanics.Spawners.NewArchitecture;
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