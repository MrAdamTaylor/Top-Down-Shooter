using System;
using DG.Tweening;
using EnterpriceLogic.Constants;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CurrencyViewWithImage : CurrencyView
{
    [SerializeField] private Image _image;
    [SerializeField] private StartScroolTween _startScroolTween;
    [SerializeField] private EndScroolTween _endScroolTween;
    private Sprite _currentSprite;
    public Transform ImageTransform => _image.transform;

    public override void UpdateCurrency(long value)
    {
        base.UpdateCurrency(value);
    }

    public void UpdateImage(Sprite sprite)
    {
        _currentSprite = sprite;
        _image.sprite = sprite;
    }

    public void SetDefaultCurrency()
    {
        _text.text = PrefabPath.DEFAULT_WEAPON_AMMO_TEXT;
    }

    public Sequence AnimateTextImageStart()
    {
        return DOTween
            .Sequence()
            .Append(_text.transform.DOScale(_startScroolTween.Scale, _startScroolTween.Duration)).SetEase(_startScroolTween.AnimationLine)
            .Insert(0,_image.transform.DOScale(_startScroolTween.Scale, _startScroolTween.Duration)).SetEase(_startScroolTween.AnimationLine);
    }

    public Sequence AnimateTextImageEnd()
    {
        return DOTween
            .Sequence()
            .Append(_text.transform.DOScale(_endScroolTween.Scale, _endScroolTween.Duration)).SetEase(_endScroolTween.AnimationLine)
            .Insert(0,_image.transform.DOScale(_endScroolTween.Scale, _endScroolTween.Duration)).SetEase(_endScroolTween.AnimationLine);
    }
}

[System.Serializable]
public struct StartScroolTween
{
    public Vector3 Scale;
    public float Duration;
    public Ease AnimationLine;
}

[System.Serializable]
public struct EndScroolTween
{
    public Vector3 Scale;
    public float Duration;
    public Ease AnimationLine;
}