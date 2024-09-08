using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInstaller : MonoBehaviour
{
    public void Awake()
    {
        var view = FindObjectOfType<CurrencyProvider>();
        AmmoBind(view.AmmoView);
        ScoresBind(view.ScoresView);
        MoneyBind(view.MoneyView);
        LoadStaticData();
    }

    private void LoadStaticData()
    {
        UIWeaponStaticDataIcons icons = Resources.Load<UIWeaponStaticDataIcons>("StaticData/UI/UIWeaponIcons");
        Debug.Log(icons);
        AmmoAdapter adapter = (AmmoAdapter)ServiceLocator.Instance.GetData(typeof(AmmoAdapter));
        adapter.PictureConstruct(icons);
    }

    private void AmmoBind(CurrencyViewWithImage view)
    {
        ServiceLocator.Instance.BindData(typeof(AmmoStorage), new AmmoStorage(10L));
        Debug.Log("Ammo Storage is Install");
        ServiceLocator.Instance.BindData(typeof(AmmoAdapter), new AmmoAdapter(
            view,
            (AmmoStorage)ServiceLocator.Instance.GetData(typeof(AmmoStorage))));
        AmmoAdapter adapter = (AmmoAdapter)ServiceLocator.Instance.GetData(typeof(AmmoAdapter));
        adapter.Initialize();
    }

    private void ScoresBind(CurrencyView view)
    {
        ServiceLocator.Instance.BindData(typeof(ScoresStorage), new ScoresStorage(10L));
        Debug.Log("Scores Storage is Install");
        ServiceLocator.Instance.BindData(typeof(ScoresAdapter), new ScoresAdapter(
            view,
            (ScoresStorage)ServiceLocator.Instance.GetData(typeof(ScoresStorage))));
        ScoresAdapter adapter = (ScoresAdapter)ServiceLocator.Instance.GetData(typeof(ScoresAdapter));
        adapter.Initialize();
    }

    private void MoneyBind(CurrencyView view)
    {
        ServiceLocator.Instance.BindData(typeof(MoneyStorage), new MoneyStorage(10L));
        Debug.Log("Money Storage is Install");
        ServiceLocator.Instance.BindData(typeof(MoneyAdapter), new MoneyAdapter(
            view,
            (MoneyStorage)ServiceLocator.Instance.GetData(typeof(MoneyStorage))));
        MoneyAdapter adapter = (MoneyAdapter)ServiceLocator.Instance.GetData(typeof(MoneyAdapter));
        adapter.Initialize();
    }
}