using UnityEngine;

public class AudioPlayerComponent : MonoBehaviour
{
    private AudioClip _shootSound;
    private ShootControlSystem _coomoonShootSystem;
    
    public void Construct(ShootControlSystem shootSystem, WeaponStaticData data)
    {
        _coomoonShootSystem = shootSystem;
        _shootSound = data.ShootSound;
        _coomoonShootSystem.ShootAction += PlayShootSound;
    }

    private void PlayShootSound()
    {
        Sound.Instance.PlaySound(_shootSound);
    }
}