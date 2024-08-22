using UnityEngine;

namespace Mechanics
{
    public class Death : MonoBehaviour
    {
        [SerializeField] private Transform _position;

        //[SerializeField] private EnemySpawner _enemySpawner;
        
        public bool isDeath;
    
        public void MakeDeath()
        {
            this.gameObject.SetActive(false);
            gameObject.transform.position = _position.position;
            isDeath = true;
        }
    }
}
