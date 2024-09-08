using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AmmoAdapter : IDisposable
{
    private readonly CurrencyViewWithImage _currencyView;
    private readonly AmmoStorage _scoresStorage;
    private Dictionary<WeaponType, Sprite> _icoImage = new();
    public AmmoAdapter(CurrencyViewWithImage view, AmmoStorage ammoStorage)
    {
        _currencyView = view;
        _scoresStorage = ammoStorage;
    }
    
    public void Initialize()
    {
        _scoresStorage.OnAmmoChanged += UpdateAmmo;
    }

    public void PictureConstruct(UIWeaponStaticDataIcons staticDataIcons)
    {
        _icoImage = staticDataIcons.IcoConfigs.ToDictionary(x => x.WeaponType, y => y.WeaponPicture);
        Debug.Log("Ico Dictionary Count "+_icoImage.Count);
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

    public void Dispose()
    {
        _scoresStorage.OnAmmoChanged -= UpdateAmmo;
    }
}