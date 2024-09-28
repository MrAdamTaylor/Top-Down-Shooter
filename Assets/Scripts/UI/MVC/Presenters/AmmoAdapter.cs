using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using EnterpriceLogic.Utilities;
using UnityEngine;


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
    private UIAnimationPlayer _animationPlayer;

    public AmmoAdapter(CurrencyViewWithImage view, UIWeaponStaticDataIcons staticDataIcons, Scripts.Player.NewWeaponControllSystem.WeaponController controller)
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
            //_animationPlayer.PlayAnimation(_animationConfigs[weaponType],OnAmmoChange, _currencyView.Image, _currencyView.PreviousValueUI);
            _ammoStorage.UpdateScreen();
        }
        else
        {
            _currencyView.SetDefaultCurrency();
        }
    }

    private void OnAmmoChange(long value)
    {
        /*_sequence?.Kill();
        if(_animationConfigs[_currentWeaponType].ValueChangeDuration.Equals(0))
            _animationPlayer.Play(_sequence, _animationConfigs[_currentWeaponType],_currencyView.ImageTransform, _currencyView.TextTransform);
        else
        {
            TweenerCore<long,long, NoOptions> tweenerCore = DOTween.To(() => _lastCurrency, Setter, value, _animationConfigs[_currentWeaponType].ValueChangeDuration);
            _animationPlayer.Play(_sequence, _animationConfigs[_currentWeaponType],_currencyView.ImageTransform, _currencyView.TextTransform, tweenerCore);
        }*/
        
        _sequence?.Kill();
        if (_animationConfigs[_currentWeaponType].MiddleDuration.Equals(0) ||
            _animationConfigs[_currentWeaponType].AnimationSequenceType == AnimationSequenceType.Start_End)
        {
            Setter(value);
            _animationPlayer.Play(_sequence, _animationConfigs[_currentWeaponType],_currencyView.ImageTransform, _currencyView.TextTransform);
        }
        else
        {
            TweenerCore<long,long, NoOptions> tweenerCore = DOTween.To(() => _lastCurrency, Setter, value, _animationConfigs[_currentWeaponType].MiddleDuration);
            _animationPlayer.Play(_sequence, _animationConfigs[_currentWeaponType],_currencyView.ImageTransform, _currencyView.TextTransform, tweenerCore);
        }
        
    }
    
    private void Setter(long value)
    {
        _currencyView.UpdateCurrency(value);
        _lastCurrency = value;
    }
}

public class UIAnimationPlayer
{

    public void Play(Sequence sequence, AnimationConfigs animationConfigs, Transform image, Transform text, TweenerCore<long,long, NoOptions> tweenerCore = null)
    {
        sequence = DOTween.Sequence();
        Vector3 imagePosition = image.position;
        Vector3 textPosition = text.position;
        if (tweenerCore.IsNull())
            sequence.Append(ReturnStarterSequence(animationConfigs.ImageConfigs.StartConfigs,
                    animationConfigs.TextConfigs.StartConfigs, text, image))
                .Append(ReturnEndedSequence(animationConfigs.ImageConfigs.EndConfigs, 
                    animationConfigs.TextConfigs.EndConfigs, text, image, textPosition, imagePosition));
        else
        {
            sequence.Append(ReturnStarterSequence(animationConfigs.ImageConfigs.StartConfigs,
                    animationConfigs.TextConfigs.StartConfigs, text, image))
                .Append(tweenerCore)
                .Append(ReturnEndedSequence(animationConfigs.ImageConfigs.EndConfigs, 
                    animationConfigs.TextConfigs.EndConfigs, text, image, textPosition, imagePosition));
        }
    }

    private Tween ReturnEndedSequence(TweenConfigs imageConfigsEndConfigs, TweenConfigs textConfigsEndConfigs, Transform text, Transform image, Vector3 textPosition, Vector3 imagePosition)
    {
        return DOTween
            .Sequence()
            .Append(text.transform.DOMove(textPosition,textConfigsEndConfigs.RillRate))
            .SetEase(textConfigsEndConfigs.EaseType)
            .Insert(0, image.transform.DOMove(imagePosition, imageConfigsEndConfigs.RillRate))
            .SetEase(imageConfigsEndConfigs.EaseType);
    }

    private Tween ReturnStarterSequence(TweenConfigs imageConfigsStartConfigs, TweenConfigs textConfigsStartConfigs, Transform text, Transform image)
    {
        return DOTween
            .Sequence()
            .Append(text.transform.DOShakePosition(textConfigsStartConfigs.RillRate, textConfigsStartConfigs.Scale, 10))
            .SetEase(textConfigsStartConfigs.EaseType)
            .Insert(0, image.transform.DOShakePosition(imageConfigsStartConfigs.RillRate, imageConfigsStartConfigs.Scale, 10))
            .SetEase(imageConfigsStartConfigs.EaseType);
    }

    private Sequence ReturnAnimationByConfigs(AnimationConfigs animationConfigs,Transform image, Transform text, TweenerCore<long, long, NoOptions> tweenerCore = null)
    {
        Vector3 imagePosition = image.position;
        Vector3 textPosition = text.position;
        
        switch (animationConfigs.AnimationSequenceType)
        {
            /*case AnimationSequenceType.EverythingIsConsistent:
                if (tweenerCore.IsNull())
                {
                    
                }
                else
                {
                    
                }

                return DOTween
                    .Sequence()
                    .Append(StartAnimationByConfigs(animationConfigs, text))
                    .Insert(0, GetImageAnimationStart(animationConfigs, image))
                    .Append(tweenerCore)
                    .Append(EndAnimationByConfigs(animationConfigs, text))
                    .Insert(0, GetImageAnimationEnd(animationConfigs, image, imagePosition));
            break;*/
            
        }

        throw new Exception("Chose TweenType for AmmoClick not Implemented");
    }

    /*private Sequence EndAnimationByConfigs(AnimationConfigs animationConfigs,Transform text )
    {
        return DOTween
            .Sequence()
            .Append(text.DOScale(animationConfigs.StartScale.Scale, animationConfigs.StartScale.RillRate));
    }

    private Sequence StartAnimationByConfigs(AnimationConfigs animationConfigs, Transform text)
    {
        return DOTween
            .Sequence()
            .Append(text.transform.DOScale(animationConfigs.EndScale.Scale, animationConfigs.EndScale.RillRate));
    }*/

    /*private Sequence GetImageAnimationEnd(AnimationConfigs animationConfigs, Transform image, Vector3 oldPosition)
    {
        switch (animationConfigs._animationType)
        {
            case AnimationType.Scale1_5x:
                return DOTween.Sequence().Append(image.transform.DOScale(animationConfigs.EndScale.Scale,
                    animationConfigs.EndScale.RillRate));
                break;
            case AnimationType.LeftRight:
                return DOTween.Sequence().Append(image.DOMove(
                    image.position - new Vector3(10f, 0.0f, 0.0f), animationConfigs.EndScale.RillRate));
                break;
            case AnimationType.TopDown:
                return DOTween.Sequence()
                    .Append(image.DOMove(oldPosition, animationConfigs.EndScale.RillRate));
            /*case ShakingPictureType.TopDown:
                return DOTween.Sequence().Append(image.DOMove(
                    image.position - new Vector3(0.0f, 10f, 0.0f), animationConfigs.EndScale.RillRate));#1#
            default:
                return null;
        }
    }*/

    /*private Sequence GetImageAnimationStart(AnimationConfigs animationConfigs, Transform image)
    {
        switch (animationConfigs._animationType)
        {
            case AnimationType.Scale1_5x:
                return DOTween.Sequence().Append(image.DOScale(animationConfigs.StartScale.Scale,
                    animationConfigs.StartScale.RillRate));
            break;
            case AnimationType.LeftRight:
                return DOTween.Sequence().Append(image.DOMove(
                    image.position + new Vector3(10f, 0.0f, 0.0f), animationConfigs.StartScale.RillRate));
            break;
            /*case ShakingPictureType.TopDown:
                return DOTween.Sequence().Append(image.DOMove(
                    image.position + new Vector3(0.0f, 10f, 0.0f), animationConfigs.StartScale.RillRate));#1#
            case AnimationType.TopDown:
                return DOTween.Sequence()
                    .Append(image.DOShakePosition(animationConfigs.StartScale.RillRate, 1F, 10, 90, false, true));
            default:
                return null;
        }
    }*/
}