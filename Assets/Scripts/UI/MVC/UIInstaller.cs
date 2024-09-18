using UnityEngine;

public class UIInstaller : MonoBehaviour
{
    private bool _selfRegistration = true;
    
    public void Awake()
    {
        /*Debug.Log("Installer - выполнился в Awake");
        if ((GameBootstraper)FindObjectOfType(typeof(GameBootstraper)))
        {
            _selfRegistration = false;
            Debug.Log("Работаем по другому");
        }
        var view = FindObjectOfType<CurrencyProvider>();
        var ammo = FindObjectOfType<AmmoProvider>();
        ScoresBind(view.ScoresView);
        MoneyBind(view.MoneyView);
        AmmoBind(ammo, view.AmmoView);
        LoadStaticData();*/
    }

    private void AmmoBind(AmmoProvider ammo, CurrencyViewWithImage view)
    {
        ServiceLocator.Instance.BindData(typeof(AmmoStorage), new AmmoStorage());
        ammo.Construct();
        ServiceLocator.Instance.BindData(typeof(AmmoAdapter), new AmmoAdapter(view));
        AmmoAdapter adapter = (AmmoAdapter)ServiceLocator.Instance.GetData(typeof(AmmoAdapter));
        for (int i = 0; i < ammo.Count; i++)
        {
            adapter.AddAmmoStorage(ammo.GetTypeStorageCortege(i));
        }
    }

    private void LoadStaticData()
    {
        UIWeaponStaticDataIcons icons = Resources.Load<UIWeaponStaticDataIcons>("StaticData/UI/UIWeaponIcons");
        AmmoAdapter adapter = (AmmoAdapter)ServiceLocator.Instance.GetData(typeof(AmmoAdapter));
        adapter.PictureConstruct(icons);
    }


    private void ScoresBind(CurrencyView view)
    {
        ServiceLocator.Instance.BindData(typeof(ScoresStorage), new ScoresStorage(10L));
        ServiceLocator.Instance.BindData(typeof(ScoresAdapter), new ScoresAdapter(
            view,
            (ScoresStorage)ServiceLocator.Instance.GetData(typeof(ScoresStorage))));
        ScoresAdapter adapter = (ScoresAdapter)ServiceLocator.Instance.GetData(typeof(ScoresAdapter));
        adapter.Initialize();
    }

    private void MoneyBind(CurrencyView view)
    {
        ServiceLocator.Instance.BindData(typeof(MoneyStorage), new MoneyStorage(10L));
        ServiceLocator.Instance.BindData(typeof(MoneyAdapter), new MoneyAdapter(
            view,
            (MoneyStorage)ServiceLocator.Instance.GetData(typeof(MoneyStorage))));
        MoneyAdapter adapter = (MoneyAdapter)ServiceLocator.Instance.GetData(typeof(MoneyAdapter));
        adapter.Initialize();
    }
}