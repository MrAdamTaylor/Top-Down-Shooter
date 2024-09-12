using System;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _speed; 
        [SerializeField] private int _health;

        [SerializeField] private float _scores;
        [SerializeField] private float _probability;

        private Health _healthComponent;
    
        //TODO - Dependency (Level - Awake+GetComponent) (class - Player)
        private void Awake()
        {
            try
            {
                _healthComponent = this.GetComponent<Health>();
                _healthComponent.MaxHealth = _health;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public float ReturnSpeed()
        {
            return _speed;
        }
    }
}