using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AmmoAdapter : IDisposable
{
    private readonly CurrencyViewWithImage _currencyView;
    private AmmoStorage _ammoStorage;
    private Dictionary<WeaponType, Sprite> _icoImage = new();
    private Dictionary<Weapon,AmmoStorage> _dataDictionary = new();

    public AmmoAdapter(CurrencyViewWithImage view)
    {
        _currencyView = view;
    }

    public void PictureConstruct(UIWeaponStaticDataIcons staticDataIcons)
    {
        _icoImage = staticDataIcons.IcoConfigs.ToDictionary(x => x.WeaponType, y => y.WeaponPicture);
    }

    public void Dispose()
    {
        _ammoStorage.OnAmmoChanged -= UpdateAmmo;
    }

    public void UpdatePicture(WeaponType weaponType)
    {
        Sprite sprite = _icoImage[weaponType];
        _currencyView.UpdateImage(sprite);
    }

    public void UpdateAmmo(long value)
    {
        _currencyView.UpdateCurrency(value);
    }

    public void AddAmmoStorage((Weapon, AmmoStorage) getTypeStorageCortege)
    {
        _dataDictionary.Add(getTypeStorageCortege.Item1,getTypeStorageCortege.Item2);
    }

    public void UpdateUI(Weapon getWeaponByType)
    {
        CleanAmmoStorageEvent();
        _ammoStorage = _dataDictionary[getWeaponByType];
        _ammoStorage.OnAmmoChanged += UpdateAmmo;
        _ammoStorage.UpdateScreen();
    }

    private void CleanAmmoStorageEvent()
    {
        _ammoStorage = null;
    }
}