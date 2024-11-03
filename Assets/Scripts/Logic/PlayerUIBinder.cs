using Configs;
using EnterpriceLogic.Constants;
using Infrastructure.ServiceLocator;
using Logic.Timer;
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

    public void BindTimer(GameTimer gameTimer, WaveTimer waveTimer)
    {
        ServiceLocator.Instance.BindData(typeof(TimerAdapter), new TimerAdapter(_currencyProvider.TimerView,gameTimer, waveTimer));
        TimerAdapter timerAdapter = (TimerAdapter)ServiceLocator.Instance.GetData(typeof(TimerAdapter));
        timerAdapter.Initialize();
        /*ServiceLocator.Instance.BindData(typeof(MoneyStorage), new MoneyStorage(Constants.STANDART_UI_VALUE));
        ServiceLocator.Instance.BindData(typeof(TimerAdapter), new TimerAdapter(
            _currencyProvider.TimerView,
            (MoneyStorage)ServiceLocator.Instance.GetData(typeof(MoneyStorage))));
        TimerAdapter timerAdapter = (TimerAdapter)ServiceLocator.Instance.GetData(typeof(TimerAdapter));
        timerAdapter.Initialize();*/
    }

    public void BindAmmo(GameObject playerObject)
    {
        UIWeaponStaticDataIcons icons = Resources.Load<UIWeaponStaticDataIcons>(PrefabPath.WEAPON_ICO_PATH);
        ServiceLocator.Instance.BindData(typeof(AmmoAdapter), new AmmoAdapter(_currencyProvider.AmmoView,icons, 
            playerObject.GetComponent<WeaponController>()));
    }

    public void BindHealth(GameObject playerObject)
    {
        ServiceLocator.Instance.BindData(typeof(HealthAdapter), 
            new HealthAdapter(playerObject.GetComponent<PlayerHealth>(), _currencyProvider.ImageFillAmountView));
    }

    public void BindWaves()
    {
        
    }


}