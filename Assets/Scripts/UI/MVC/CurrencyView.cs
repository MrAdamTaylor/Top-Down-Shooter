using System;
using TMPro;
using UnityEngine;

public class CurrencyView : MonoBehaviour
{
   [SerializeField] private TMP_Text _text;

   private long _currentValueUI;


   private void Start()
   {
      _text.text = Constants.DEFAULT_WEAPON_AMMO_TEXT;
   }


   public virtual void UpdateCurrency(long value)
   {
         if (value == -long.MaxValue)
         {
            _text.text = Constants.DEFAULT_WEAPON_AMMO_TEXT;
         }
         else
         {
            _currentValueUI = value;
            _text.text = _currentValueUI.ToString();
         }
   }
}