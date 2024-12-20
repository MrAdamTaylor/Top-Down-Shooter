using Player.ShootSystem;
using UnityEngine;
using Weapon.StaticData;

namespace Infrastructure.Services.AbstractFactory
{
    public class AudioPlayerComponent : MonoBehaviour
    {
        private AudioClip[] _shootSounds;
        private ShootControlSystem _coomoonShootSystem;

        public void Construct(ShootControlSystem shootSystem, WeaponStaticData data)
        {
            _coomoonShootSystem = shootSystem;
            _shootSounds = data.ShootSound;
            _coomoonShootSystem.ShootAction += PlayShootSound;
        }

        private void PlayShootSound()
        {
            Sound.Sound.Instance.PlaySound(_shootSounds[Random.Range(0, _shootSounds.Length)]);
        }
    }
}