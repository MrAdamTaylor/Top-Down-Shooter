using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CurrencyViewWithImage : CurrencyView
{
    [SerializeField] private Image _image;

    private Sprite _currentSprite;

    public override void UpdateCurrency(long value)
    {
        base.UpdateCurrency(value);
    }

    public void UpdateImage(Sprite sprite)
    {
        _currentSprite = sprite;
        _image.sprite = sprite;
    }
}