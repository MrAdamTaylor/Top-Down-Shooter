using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyProvider : MonoBehaviour
{
    [SerializeField] private CurrencyViewWithImage _ammoView;
    [SerializeField] private CurrencyView _scoresView;
    [SerializeField] private CurrencyView _moneyView;


    public CurrencyView ScoresView => _scoresView;
    public CurrencyViewWithImage AmmoView => _ammoView;
    public CurrencyView MoneyView => _moneyView;
}
