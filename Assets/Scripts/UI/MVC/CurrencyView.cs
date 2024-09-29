using EnterpriceLogic.Constants;
using TMPro;
using UnityEngine;

public class CurrencyView : MonoBehaviour
{
   [SerializeField] protected TMP_Text _text;

   public Transform TextTransform => _text.transform;
   private long _currentValueUI = 0;

   private void Start()
   {
      _text.text = PrefabPath.DEFAULT_WEAPON_AMMO_TEXT;
   }


   public virtual void UpdateCurrency(long value)
   {
         if (value == -long.MaxValue)
         {
            _text.text = PrefabPath.DEFAULT_WEAPON_AMMO_TEXT;
         }
         else
         {
            _currentValueUI = value;
            _text.text = _currentValueUI.ToString();
         }
   }
}