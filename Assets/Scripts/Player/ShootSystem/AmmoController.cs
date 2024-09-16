using System;
using UnityEngine;

[RequireComponent(typeof(ShootControlSystem), typeof(Weapon))]
public class AmmoController : MonoBehaviour
{
    public Action<long, bool> ChangeAmmo;

    [SerializeField] private long _ammoCount;
    [SerializeField] private bool _infinity;
    [SerializeField] private ShootControlSystem _shootControlSystem;
    [SerializeField] private long _ammoWaste = Constants.STANDART_WASTE_WALUE;
    
    private long _currentAmmo;
    private AmmoStorage _ammoStorage;
    private Weapon _weapon;

    public void Construct(ShootControlSystem _shootControlSystem)
    {
        _weapon = gameObject.GetComponent<Weapon>();
        _ammoStorage = (AmmoStorage)ServiceLocator.Instance.GetCloneData(typeof(AmmoStorage));
        if (_infinity)
        {
            _currentAmmo = -long.MaxValue;
        }
        else
        {
            _currentAmmo = _ammoCount;
        }

        _ammoStorage.Construct(_currentAmmo);
    }

    void Awake()
    {
        _currentAmmo = _ammoCount;
    }

    void Start()
    {
        _shootControlSystem.ShootAction += WasteAmmo;
    }

    void OnDestroy()
    {
        _shootControlSystem.ShootAction -= WasteAmmo;
    }


    public bool CanShoot()
    {
        if (_currentAmmo > 0 || _infinity)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddAmmo(long ammoBonus)
    {
        if (_infinity)
        {
            return;
        }
        else
        {
            ChangeAmmo?.Invoke(_currentAmmo, _infinity);
            _currentAmmo += ammoBonus;
            _ammoStorage.AddAmmo(ammoBonus);
        }
    }

    public Weapon GetWeaponType()
    {
        return _weapon;
    }

    public AmmoStorage GetStorage()
    {
        return _ammoStorage;
    }

    private void WasteAmmo()
    {
        if (!_infinity)
        {
            _currentAmmo -= _ammoWaste;
            ChangeAmmo?.Invoke(_currentAmmo, _infinity);
            _ammoStorage.SpendAmmo(_ammoWaste);
        }
        else
        {
            return;
        }
    }
}