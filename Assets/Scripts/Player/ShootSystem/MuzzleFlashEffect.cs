using EnterpriceLogic.Constants;
using EnterpriceLogic.Utilities;
using UnityEngine;

[RequireComponent(typeof(ShootControlSystem))]
public class MuzzleFlashEffect : MonoBehaviour
{
    private Transform _position;
    private ParticleSystem _effect;
    private ShootControlSystem _shootControlSystem;

    public void Construct(ShootControlSystem shootControlSystem, WeaponEffectsConteiner conteiner, Transform data)
    {
        _shootControlSystem = shootControlSystem;
        _shootControlSystem.ShootAction += MuzzleEffect;
        _position = data;
        _effect = conteiner.GetParticleEffect(PrefabPath.MUZZLE_FLASH_WEAPON_PATH, _position, gameObject.transform);
    }

    private void OnEnable()
    {
        if (!_effect.IsNullBoolWarning("MuzzleEffectIsNull"))
            _effect.Stop();
        else
        {
            _effect = null;
        }
    }

    private void OnDestroy()
    {
        _shootControlSystem.ShootAction -= MuzzleEffect;
    }

    private void MuzzleEffect()
    {
            if (_effect.isPlaying)
            {
                _effect.Stop();
                _effect.Play();
            }
            else
            {
                _effect.Play();
            }
    }
}
