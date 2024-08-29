using UnityEngine;

[RequireComponent(typeof(ShootControlSystem))]
public class MuzzleFlashEffect : MonoBehaviour
{
    [SerializeField] private Transform _position;
    [SerializeField] private ParticleSystem _particleSystem;
    private ParticleSystem _effect;
    private ShootControlSystem _shootControlSystem;
    private void Awake()
    {
        _shootControlSystem = this.gameObject.GetComponent<ShootControlSystem>();
        _shootControlSystem.ShootAction += MuzzleEffect;
    }
    private void OnEnable()
    {
        if (_effect != null)
        {
            _effect.Stop();
        }
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

    private void OnDestroy()
    {
        _shootControlSystem.ShootAction -= MuzzleEffect;
    }
}
