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

        public GameObject  CreateWithLoadConnect(object popupPath, object parent)
        {
            if (popupPath is not string path)
            {
                throw new InvalidDataException("Expected string type for Player Popup path");
            }

            if (parent is not GameObject parentTransform)
            {
                throw new InvalidDataException("Expected CurrencyProvider type for providers");
            }
            

            RectTransform rectTransform = parentTransform.GetComponent<RectTransform>();
            Vector3 position = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, rectTransform.localPosition.z);
        
            GameObject ui  = _assertObj.Assert(path, position);
        

            ui.transform.SetParent(parentTransform.transform, false); 
            return ui;
        }

        public GameObject CreateResetButton(object provider)
        {
            if (provider is not GameObject parentTransform)
            {
                throw new InvalidDataException("Expected CurrencyProvider type for providers");
            }
            
            RectTransform rectTransform = parentTransform.GetComponent<RectTransform>();
            Vector3 position = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, rectTransform.localPosition.z);
        
            GameObject ui  = _assertObj.Assert(PrefabPath.RESET_MENU_UI, position);
            ui.transform.SetParent(parentTransform.transform, false);
            return ui;
        }
    }
}