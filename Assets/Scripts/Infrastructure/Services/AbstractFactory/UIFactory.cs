using System.IO;
using Configs;
using EnterpriceLogic.Constants;
using Infrastructure.Services.AssertService;
using Player;
using Player.NewWeaponControllSystem;
using UI.MVC;
using UI.MVC.Model;
using UI.MVC.Presenters;
using UnityEngine;

namespace Infrastructure.Services.AbstractFactory
{
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
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(ScoresStorage), new ScoresStorage(Constants.STANDART_UI_VALUE));
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(ScoresAdapter), new ScoresAdapter(
                currencyProvider.ScoresView,
                (ScoresStorage)ServiceLocator.ServiceLocator.Instance.GetData(typeof(ScoresStorage))));
            ScoresAdapter scoresAdapter = (ScoresAdapter)ServiceLocator.ServiceLocator.Instance.GetData(typeof(ScoresAdapter));
            scoresAdapter.Initialize();
            #endregion

            #region BindMoney
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(MoneyStorage), new MoneyStorage(Constants.STANDART_UI_VALUE));
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(MoneyAdapter), new MoneyAdapter(
                currencyProvider.MoneyView,
                (MoneyStorage)ServiceLocator.ServiceLocator.Instance.GetData(typeof(MoneyStorage))));
            MoneyAdapter moneyAdapter = (MoneyAdapter)ServiceLocator.ServiceLocator.Instance.GetData(typeof(MoneyAdapter));
            moneyAdapter.Initialize();
            #endregion
        
            #region BindAmmo
            UIWeaponStaticDataIcons icons = Resources.Load<UIWeaponStaticDataIcons>(PrefabPath.WEAPON_ICO_PATH);
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(AmmoAdapter), new AmmoAdapter(currencyProvider.AmmoView,icons, 
                playerObject.GetComponent<WeaponController>()));
            #endregion
            #region BindPlayerHP
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(HealthAdapter), 
                new HealthAdapter(playerObject.GetComponent<PlayerHealth>(), currencyProvider.HpBar));
            #endregion
            return ui;
        }
    }
}