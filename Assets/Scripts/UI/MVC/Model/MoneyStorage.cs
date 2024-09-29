using System;

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
        OnMoneyChanged?.Invoke(Money);
    }

    public void SpendMoney(long money)
    {
        Money -= money;
        OnMoneyChanged?.Invoke(Money);
    }
}