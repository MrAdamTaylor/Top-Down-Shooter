using System;
using System.Collections.Generic;
using System.Linq;
using Configs;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Infrastructure.ServiceLocator;
using Infrastructure.Services.AbstractFactory;
using Player.NewWeaponControllSystem;
using Player.ShootSystem;
using UI.MVC.Model;
using UnityEngine;
using Weapon;

namespace UI.MVC.Presenters
{
    public class AmmoAdapter : IDisposable
    {
        private CurrencyViewWithImage _currencyView;
        private AmmoStorage _ammoStorage;
        private Dictionary<WeaponType, Sprite> _icoImage = new();
        private Dictionary<WeaponType,AnimationConfigs> _animationConfigs;
        private Dictionary<WeaponType,AmmoStorage> _dataDictionary = new();

        private long _lastCurrency;
        private Sequence _sequence;
        private WeaponType _currentWeaponType;
        private readonly UIAnimationPlayer _animationPlayer;

        public AmmoAdapter(CurrencyViewWithImage view, UIWeaponStaticDataIcons staticDataIcons, WeaponController controller)
        {
            _currencyView = view;
            _icoImage = staticDataIcons.IcoConfigs.ToDictionary(x => x.WeaponType, y => y.WeaponPicture);
            _animationConfigs = staticDataIcons.IcoConfigs.ToDictionary(x => x.WeaponType, y => y.AnimationConfigs);
            WeaponData data = (WeaponData)ServiceLocator.Instance.GetData(typeof(WeaponData));
            Dictionary<int, (WeaponType, AmmoController)> dictionary = data.GetAmmoData();
            _dataDictionary = dictionary.ToDictionary(x => x.Value.Item1, y => y.Value.Item2.ReturnStorage());
            controller.ConstructUI(this);
            _animationPlayer = (UIAnimationPlayer)ServiceLocator.Instance.GetData(typeof(UIAnimationPlayer));
        }

        public void Dispose()
        {
            _ammoStorage.OnAmmoChanged -= OnAmmoChange;
        }

        public void UpdatePicture(WeaponType weaponType)
        {
            _currentWeaponType = weaponType;
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
                _ammoStorage.OnAmmoChanged += OnAmmoChange;
                _ammoStorage.UpdateScreen();
            }
            else
            {
                _currencyView.SetDefaultCurrency();
            }
        }

        private void OnAmmoChange(long value)
        {
            if (_animationConfigs[_currentWeaponType].MiddleDuration.Equals(0) ||
                _animationConfigs[_currentWeaponType].AnimationSequenceType == AnimationSequenceType.Start_End)
            {
                Setter(value);
                _animationPlayer.Play(_animationConfigs[_currentWeaponType],_currencyView.ImageTransform, _currencyView.TextTransform);
            }
            else
            {
                TweenerCore<long,long, NoOptions> tweenerCore = DOTween.To(() => _lastCurrency, Setter, value, _animationConfigs[_currentWeaponType].MiddleDuration);
                _animationPlayer.Play(_animationConfigs[_currentWeaponType],_currencyView.ImageTransform, _currencyView.TextTransform, tweenerCore);
            }
        }
    
        private void Setter(long value)
        {
            _currencyView.UpdateCurrency(value);
            _lastCurrency = value;
        }
    }
}