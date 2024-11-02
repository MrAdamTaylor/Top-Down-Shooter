using UnityEngine.UI;

namespace UI.MVC
{
    public class ImageFillAmountView : CurrencyView
    {
        public Image ImageCurrent;

        public void SetValue(float current, float max) 
            => ImageCurrent.fillAmount = current / max;
    }
}