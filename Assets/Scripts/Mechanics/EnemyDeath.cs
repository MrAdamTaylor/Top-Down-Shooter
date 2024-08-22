using System;
using Mechanics.Spawners.NewArchitecture;
using UnityEngine;

namespace Mechanics
{
    public class EnemyDeath : MonoBehaviour
    {
        //[SerializeField] private Transform _position;

        //[SerializeField] private EnemySpawner _enemySpawner;

        public event Action OnDeath;
        //public bool isDeath;
    
        public void MakeDeath()
        {
            //_enemySpawner.PoolReturn += ReturnObject(this.gameObject);
            //this.gameObject.SetActive(false);
            OnDeath?.Invoke();
            //gameObject.transform.position = _position.position;
            //isDeath = true;
        }
    }
}