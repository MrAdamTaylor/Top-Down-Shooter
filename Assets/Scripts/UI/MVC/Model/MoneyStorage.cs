using System;
using UnityEngine;

public class MoneyStorage
{
    public event Action<long> OnMoneyChanged;
        
    public long Money { get; private set; }

    public MoneyStorage(long money)
    {
        Money = money;
    }

    public void AddMoney(long money)
    {
        Money += money;
        Debug.Log("Current Money: "+Money);
        OnMoneyChanged?.Invoke(Money);
    }

    public void SpendMoney(long money)
    {
        Money -= money;
        Debug.Log("Current Money: "+Money);
        OnMoneyChanged?.Invoke(Money);
    }
}