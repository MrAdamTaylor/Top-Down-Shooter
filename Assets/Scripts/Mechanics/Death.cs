using UnityEngine;

namespace Mechanics
{
    public class Death : MonoBehaviour
    {
        [SerializeField] private Transform _position;
        
        public bool IsDeath;
    
        public void MakeDeath()
        {
            gameObject.SetActive(false);
            gameObject.transform.position = _position.position;
            IsDeath = true;
        }
    }
}
