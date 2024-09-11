using TMPro;
using UnityEngine;

public class CurrencyView : MonoBehaviour
{
   [SerializeField] private TMP_Text _text;

   private long _currentValueUI;

   public void UpdateCurrency(long value)
   {
      if (value == -long.MaxValue)
      {
         _text.text = "Infinity";
      }
      else
      {
         _currentValueUI = value;
         _text.text = _currentValueUI.ToString();
      }
   }
}