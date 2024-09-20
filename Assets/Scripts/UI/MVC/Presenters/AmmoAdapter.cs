using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;

public class AmmoAdapter : IDisposable
{
    private CurrencyViewWithImage _currencyView;
    private AmmoStorage _ammoStorage;
    private Dictionary<WeaponType, Sprite> _icoImage = new();
    private Dictionary<WeaponType,AmmoStorage> _dataDictionary = new();


    public AmmoAdapter()
    {
        
    }

    public AmmoAdapter(CurrencyViewWithImage view, UIWeaponStaticDataIcons staticDataIcons)
    {
        _currencyView = view;
        _icoImage = staticDataIcons.IcoConfigs.ToDictionary(x => x.WeaponType, y => y.WeaponPicture);
        WeaponData data = (WeaponData)ServiceLocator.Instance.GetData(typeof(WeaponData));
        Dictionary<int, (WeaponType, AmmoController)> dictionary = data.GetAmmoData();
        _dataDictionary = dictionary.ToDictionary(x => x.Value.Item1, y => y.Value.Item2.ReturnStorage());
        Debug.Log("Ammo Adapter Dictionary: "+_dataDictionary.Count);
    }

    public void Construct(CurrencyViewWithImage view)
    {
        _currencyView = view;
    }


    /*public void PictureConstruct(UIWeaponStaticDataIcons staticDataIcons)
    {
        _icoImage = staticDataIcons.IcoConfigs.ToDictionary(x => x.WeaponType, y => y.WeaponPicture);
    }*/

    public void Dispose()
    {
        _ammoStorage.OnAmmoChanged -= UpdateAmmo;
    }

    public void UpdatePicture(WeaponType weaponType)
    {
        Sprite sprite = _icoImage[weaponType];
        _currencyView.UpdateImage(sprite);
    }

    public void AddAmmoStorage((Weapon, AmmoStorage) getTypeStorageCortege)
    {
        //_dataDictionary.Add(getTypeStorageCortege.Item1,getTypeStorageCortege.Item2);
    }

    public void UpdateUI(Weapon getWeaponByType)
    {
        CleanAmmoStorageEvent();
        //_ammoStorage = _dataDictionary[getWeaponByType];
        _ammoStorage.OnAmmoChanged += UpdateAmmo;
        _ammoStorage.UpdateScreen();
    }

    private void UpdateAmmo(long value)
    {
        _currencyView.UpdateCurrency(value);
    }

    private void CleanAmmoStorageEvent()
    {
        _ammoStorage = null;
    }
}