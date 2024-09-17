using System.Reflection.Emit;
using EnterpriceLogic.Utilities;
using Scripts.Player.NewWeaponControllSystem;
using UnityEngine;

[RequireComponent(typeof(ShootControlSystem))]
public class MuzzleFlashEffect : MonoBehaviour
{
    [SerializeField] private Transform _position;
    [SerializeField] private ParticleSystem _particleSystem;
    private ParticleSystem _effect;
    private ShootControlSystem _shootControlSystem;
    /*void Awake()
    {
        _shootControlSystem = gameObject.GetComponent<ShootControlSystem>();
        _shootControlSystem.ShootAction += MuzzleEffect;
    }*/

    public void Construct(ShootControlSystem shootControlSystem, WeaponEffectsConteiner conteiner, Transform data)
    {
        _shootControlSystem = shootControlSystem;
        _shootControlSystem.ShootAction += MuzzleEffect;
        _position = data;
        _effect = conteiner.GetParticleEffect(Constants.MUZZLE_FLASH_WEAPON_PATH, _position, gameObject.transform);
    }

    void OnEnable()
    {
        if (!_effect.IsNullBoolWarning("MuzzleEffectIsNull"))
            _effect.Stop();
    }

    void OnDestroy()
    {
        _shootControlSystem.ShootAction -= MuzzleEffect;
    }

    private void MuzzleEffect()
    {
        if (_effect == null)
        {
            _effect = Instantiate(_particleSystem, _position.position, Quaternion.identity);
            _effect.gameObject.transform.parent = _position;
            _effect.Play();
        }
        else
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
}
