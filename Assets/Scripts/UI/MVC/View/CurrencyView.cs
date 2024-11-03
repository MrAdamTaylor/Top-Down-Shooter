using TMPro;
using UnityEngine;

namespace UI.MVC.View
{
   public class CurrencyView : MonoBehaviour, IView
   {
      [SerializeField] protected TMP_Text _text;

      public Transform TextTransform => _text.transform;
      private long _currentValueUI = 0;

      public void UpdateCurrency(string value)
      {
         _text.text = value;
      }

      public virtual void UpdateCurrency(long value)
      {
         _text.text = value.ToString();
      }
   }
}