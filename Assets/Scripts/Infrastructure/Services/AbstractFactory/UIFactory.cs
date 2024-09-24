using System.IO;
using UnityEngine;

public class UIFactory : IUIFactory
{
    private readonly IAsserts _assert;

    public UIFactory(IAsserts asserts)
    {
        _assert = asserts;
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

        GameObject ui = _assert.Instantiate(path, position);
        ui.transform.SetParent(parentTransform.transform, false); 
        //parent = parentTransform.transform;
        RectTransform rt = ui.GetComponent<RectTransform>();
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, Constants.SCREEN_OVERLAY_WIDTH);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, Constants.SCREEN_OVERLAY_HEIGHT);
        //transform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, transform.rect.width);
        //transform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, transform.rect.height);

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
        //helper.Construct();
        #endregion
        
        #region BindAmmo
        
        UIWeaponStaticDataIcons icons = Resources.Load<UIWeaponStaticDataIcons>(Constants.WEAPON_ICO_PATH);
        ServiceLocator.Instance.BindData(typeof(AmmoAdapter), new AmmoAdapter(currencyProvider.AmmoView,icons, 
            playerObject.GetComponent<Scripts.Player.NewWeaponControllSystem.WeaponController>()));
        
        
        #endregion
        return ui;
    }
}