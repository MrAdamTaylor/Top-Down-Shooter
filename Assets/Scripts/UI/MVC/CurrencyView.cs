using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyView : MonoBehaviour
{
   [SerializeField] private TMP_Text _text;

   private long _currentValueUI;

   public void UpdateCurrency(long value)
   {
      _currentValueUI = value;
      _text.text = _currentValueUI.ToString();
   }
}