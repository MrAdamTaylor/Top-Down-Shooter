using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MVC
{
    public class ImageColorView : CurrencyView
    {
        [SerializeField] private Image _image;
        [SerializeField] private Color _color;

        [SerializeField]private ScaleChange _scaleChange;

        private Color _originalColor;

        private void Awake()
        {
            _originalColor = _text.color;
        }

        public void SwitchColor()
        {
            _text.color = _color;
        }

        public void ReturnOriginalColor()
        {
            _text.color = _originalColor;
        }
    }
    [Serializable]
    public struct ScaleChange
    {
        public float DurationScale;
        public Vector3 MaxScale;
    }
}