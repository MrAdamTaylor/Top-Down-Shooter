using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyProvider : MonoBehaviour
{
    [SerializeField] private CurrencyView _ammoView;
    [SerializeField] private CurrencyView _scoresView;
    [SerializeField] private CurrencyView _moneyView;


    public CurrencyView ScoresView => _scoresView;
    public CurrencyView AmmoView => _ammoView;
    public CurrencyView MoneyView => _moneyView;
}
