using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHelper : MonoBehaviour
{
    [SerializeField] private long _current;

    private MoneyStorage _moneyStorage;
    private AmmoStorage _ammoStorage;
    private ScoresStorage _scoresStorage;
    
    public void Start()
    {
        _ammoStorage = (AmmoStorage)ServiceLocator.Instance.GetData(typeof(AmmoStorage));
        _moneyStorage = (MoneyStorage)ServiceLocator.Instance.GetData(typeof(MoneyStorage));
        _scoresStorage = (ScoresStorage)ServiceLocator.Instance.GetData(typeof(ScoresStorage));
    }

    public void AddMoney()
    {
        _moneyStorage.AddMoney(_current);
    }

    public void SpendMoney()
    {
        _moneyStorage.SpendMoney(_current);
    }
    
    public void AddAmmo()
    {
        _ammoStorage.AddAmmo(_current);
    }

    public void SpendAmmo()
    {
        _ammoStorage.SpendAmmo(_current);
    }
    
    public void AddScores()
    {
        _scoresStorage.AddScores(_current);
    }
    public void SpendScores()
    {
        _scoresStorage.SpendScores(_current);
    }
}