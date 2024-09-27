using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using DG.Tweening;
using EnterpriceLogic.Utilities;
using UnityEngine;

public class AmmoAdapter : IDisposable
{
    private CurrencyViewWithImage _currencyView;
    private AmmoStorage _ammoStorage;
    private Dictionary<WeaponType, Sprite> _icoImage = new();
    private Dictionary<WeaponType,AmmoStorage> _dataDictionary = new();
    
    private Sequence _sequence;

    public AmmoAdapter(CurrencyViewWithImage view, UIWeaponStaticDataIcons staticDataIcons, Scripts.Player.NewWeaponControllSystem.WeaponController controller)
    {
        _currencyView = view;
        _icoImage = staticDataIcons.IcoConfigs.ToDictionary(x => x.WeaponType, y => y.WeaponPicture);
        WeaponData data = (WeaponData)ServiceLocator.Instance.GetData(typeof(WeaponData));
        Dictionary<int, (WeaponType, AmmoController)> dictionary = data.GetAmmoData();
        _dataDictionary = dictionary.ToDictionary(x => x.Value.Item1, y => y.Value.Item2.ReturnStorage());
        controller.ConstructUI(this);
    }

    public void Dispose()
    {
        _ammoStorage.OnAmmoChanged -= UpdateAmmo;
    }

    public void UpdatePicture(WeaponType weaponType)
    {
        
        Sprite sprite = _icoImage[weaponType];
        _currencyView.UpdateImage(sprite);
        _sequence?.Kill();
        _sequence = DOTween.Sequence();
        _sequence.Append(_currencyView.AnimateTextImageStart());
        _sequence.Append(_currencyView.AnimateTextImageEnd());
    }

    public void UpdateUI(WeaponType weaponType)
    {
        if (_dataDictionary.ContainsKey(weaponType))
        {
            _ammoStorage = _dataDictionary[weaponType];
            _ammoStorage.OnAmmoChanged += UpdateAmmo;
            _ammoStorage.UpdateScreen();
        }
        else
        {
            _currencyView.SetDefaultCurrency();
        }
    }

    private void UpdateAmmo(long value)
    {
        _currencyView.UpdateCurrency(value);
    }
    
}