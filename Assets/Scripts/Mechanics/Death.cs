using UnityEngine;

namespace Mechanics
{
    public class Death : MonoBehaviour
    {
        [SerializeField] private Transform _position;

        public bool isDeath;
    
        public void MakeDeath()
        {
            this.gameObject.SetActive(false);
            gameObject.transform.position = _position.position;
            isDeath = true;
        }
    
    
    }
}
