using UnityEngine;
using UnityEngine.UI;

public class CurrencyViewWithImage : CurrencyView
{
    [SerializeField] private Image _image;

    private Sprite _currentSprite;
   
    public void UpdateImage(Sprite sprite)
    {
        _currentSprite = sprite;
        _image.sprite = sprite;
    }
}