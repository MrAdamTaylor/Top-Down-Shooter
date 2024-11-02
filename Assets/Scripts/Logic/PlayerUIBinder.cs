using Configs;
using EnterpriceLogic.Constants;
using Infrastructure.ServiceLocator;
using Player;
using Player.NewWeaponControllSystem;
using UI.MVC;
using UI.MVC.Model;
using UI.MVC.Presenters;
using UnityEngine;

public class PlayerUIBinder
{
    private readonly CurrencyProvider _currencyProvider;
    
    public PlayerUIBinder(CurrencyProvider currencyProvider)
    {
        _currencyProvider = currencyProvider;
    }

    public void BindScores()
    {
        ServiceLocator.Instance.BindData(typeof(ScoresStorage), new ScoresStorage(Constants.STANDART_UI_VALUE));
        ServiceLocator.Instance.BindData(typeof(ScoresAdapter), new ScoresAdapter(
            _currencyProvider.ScoresView,
            (ScoresStorage)ServiceLocator.Instance.GetData(typeof(ScoresStorage))));
        ScoresAdapter scoresAdapter = (ScoresAdapter)ServiceLocator.Instance.GetData(typeof(ScoresAdapter));
        scoresAdapter.Initialize();
    }

    public void BindMoney()
    {
        ServiceLocator.Instance.BindData(typeof(MoneyStorage), new MoneyStorage(Constants.STANDART_UI_VALUE));
        ServiceLocator.Instance.BindData(typeof(MoneyAdapter), new MoneyAdapter(
            _currencyProvider.TimerView,
            (MoneyStorage)ServiceLocator.Instance.GetData(typeof(MoneyStorage))));
        MoneyAdapter moneyAdapter = (MoneyAdapter)ServiceLocator.Instance.GetData(typeof(MoneyAdapter));
        moneyAdapter.Initialize();
    }

    public void BindAmmo(GameObject playerObject)
    {
        UIWeaponStaticDataIcons icons = Resources.Load<UIWeaponStaticDataIcons>(PrefabPath.WEAPON_ICO_PATH);
        ServiceLocator.Instance.BindData(typeof(AmmoAdapter), new AmmoAdapter(_currencyProvider.AmmoView,icons, 
            playerObject.GetComponent<WeaponController>()));
        
        ServiceLocator.Instance.BindData(typeof(HealthAdapter), 
            new HealthAdapter(playerObject.GetComponent<PlayerHealth>(), _currencyProvider.ImageFillAmountView));
    }



    
}