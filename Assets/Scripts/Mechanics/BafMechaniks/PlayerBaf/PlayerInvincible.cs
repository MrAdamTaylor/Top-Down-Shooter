using System.Collections;
using UnityEngine;

namespace Mechanics.BafMechaniks
{
    public class PlayerInvincible : MonoBehaviour, IPlayerBonusComponent
    {
        [SerializeField] private float _liefTime = 10f;
        
        void Awake()
        {
            StartCoroutine(Waiter());
        }

        private IEnumerator Waiter()
        {
            yield return new WaitForSeconds(_liefTime);
            Destroy(this);
        }
    }
}