using UnityEngine;

namespace Mechanics.BafMechaniks
{
    public class PlayerBafBonus : Bonus
    {

        [SerializeField] private PlayerBonus _playerBonus;
        private Player _player;
        
        void Start()
        {
            _player = (Player)ServiceLocator.Instance.GetData(typeof(Player));
            Subscribe();
        }
        
        private void Subscribe()
        {
            _touchTriger.OnTouch += AddPlayerBonus;
        }

        private void AddPlayerBonus()
        {
            switch (_playerBonus)
            {
                case PlayerBonus.Invincible:
                    _player.AddBonus<PlayerInvincible>();
                    break;
                case PlayerBonus.Speed:
                    _player.AddBonus<PlayerSpeed>();
                    break;
            }
        }

        private void OnDestroy()
        {
            _touchTriger.OnTouch -= AddPlayerBonus;
        }
    }
}