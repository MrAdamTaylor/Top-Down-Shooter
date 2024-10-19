using System.IO;
using EnterpriceLogic.Constants;
using Infrastructure.Services.AssertService.ExtendetAssertService;
using UnityEngine;

public class UIFactory : IUIFactory
{
    private readonly IAssertByString<GameObject> _assertObj;

    public UIFactory(AssertBuilder builder)
    {
        _assertObj = builder.BuildAssertServiceByString<GameObject>();
    }

    public GameObject CreateWithLoadConnect(object popupPath, object provider, object player)
    {
        if (popupPath is not string path)
        {
            throw new InvalidDataException("Expected string type for Player Popup path");
        }

        if (provider is not GameObject parentTransform)
        {
            throw new InvalidDataException("Expected CurrencyProvider type for providers");
        }

        if (player is not GameObject playerObject)
        {
            throw new InvalidDataException("Expected GameObject for connected object");
        }

        RectTransform rectTransform = parentTransform.GetComponent<RectTransform>();
        Vector3 position = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, rectTransform.localPosition.z);
        
        GameObject ui  = _assertObj.Assert(path, position);
        

        ui.transform.SetParent(parentTransform.transform, false); 

        CurrencyProvider currencyProvider = ui.transform.GetComponentInChildren<CurrencyProvider>();
        #region BindScores
        ServiceLocator.Instance.BindData(typeof(ScoresStorage), new ScoresStorage(Constants.STANDART_UI_VALUE));
        ServiceLocator.Instance.BindData(typeof(ScoresAdapter), new ScoresAdapter(
            currencyProvider.ScoresView,
            (ScoresStorage)ServiceLocator.Instance.GetData(typeof(ScoresStorage))));
        ScoresAdapter scoresAdapter = (ScoresAdapter)ServiceLocator.Instance.GetData(typeof(ScoresAdapter));
        scoresAdapter.Initialize();
        #endregion

        #region BindMoney
        ServiceLocator.Instance.BindData(typeof(MoneyStorage), new MoneyStorage(Constants.STANDART_UI_VALUE));
        ServiceLocator.Instance.BindData(typeof(MoneyAdapter), new MoneyAdapter(
            currencyProvider.MoneyView,
            (MoneyStorage)ServiceLocator.Instance.GetData(typeof(MoneyStorage))));
        MoneyAdapter moneyAdapter = (MoneyAdapter)ServiceLocator.Instance.GetData(typeof(MoneyAdapter));
        moneyAdapter.Initialize();
        #endregion
        
        #region BindAmmo
        UIWeaponStaticDataIcons icons = Resources.Load<UIWeaponStaticDataIcons>(PrefabPath.WEAPON_ICO_PATH);
        ServiceLocator.Instance.BindData(typeof(AmmoAdapter), new AmmoAdapter(currencyProvider.AmmoView,icons, 
            playerObject.GetComponent<Scripts.Player.NewWeaponControllSystem.WeaponController>()));
        #endregion
        #region BindPlayerHP
        ServiceLocator.Instance.BindData(typeof(HealthAdapter), 
            new HealthAdapter(playerObject.GetComponent<PlayableHealth>(), currencyProvider.HpBar));
        PlayableHealth playableHealth = playerObject.GetComponent<PlayableHealth>();
        playableHealth.Construct(Constants.PLAYER_HP);
        #endregion
        return ui;
    }
}