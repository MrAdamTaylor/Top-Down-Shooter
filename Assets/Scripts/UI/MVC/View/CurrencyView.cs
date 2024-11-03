using TMPro;
using UnityEngine;

namespace UI.MVC
{
   public class CurrencyView : MonoBehaviour, IView
   {
      [SerializeField] protected TMP_Text _text;

      public Transform TextTransform => _text.transform;
      private long _currentValueUI = 0;

      /*private void Start()
      {
         _text.text = PrefabPath.DEFAULT_WEAPON_AMMO_TEXT;
      }*/

      public void UpdateCurrency(string value)
      {
         _text.text = value;
      }

      public virtual void UpdateCurrency(long value)
      {
         _text.text = value.ToString();
         /*if (value == -long.MaxValue)
         {
            _text.text = PrefabPath.DEFAULT_WEAPON_AMMO_TEXT;
         }
         else
         {
            _currentValueUI = value;
            _text.text = _currentValueUI.ToString();
         }*/
      }
   }
}