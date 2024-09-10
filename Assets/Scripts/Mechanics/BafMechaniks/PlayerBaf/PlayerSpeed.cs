using System.Collections;
using UnityEngine;

namespace Mechanics.BafMechaniks
{
    public class PlayerSpeed : MonoBehaviour, IPlayerBonusComponent
    {
        [SerializeField] private float _liefTime = 5f;
        [SerializeField] private float _speedKoef = -2f;
        private Player _player;
        
        
        public void Awake()
        {
            _player = this.gameObject.GetComponent<Player>();
            _player.SwitchSpeed(_speedKoef);
            StartCoroutine(Waiter());
        }

        private IEnumerator Waiter()
        {
            yield return new WaitForSeconds(_liefTime);
            _player.SwitchSpeed(Constants.STANDART_VALUE_FOR_SPEED);
            Destroy(this);
        }
    }
}