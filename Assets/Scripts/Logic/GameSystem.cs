using System;
using EnterpriceLogic.Constants;
using Infrastructure;
using Infrastructure.BootstrapLogic;
using Infrastructure.ServiceLocator;
using Infrastructure.Services;
using Infrastructure.StateMachine.States;
using UnityEngine;

namespace Logic
{
    public class GameSystem : MonoBehaviour
    {
        private GameObject _resetMenu;

        private GameObject _bootstraper;
        private ISceneLoader _sceneLoader;
        private GameTimeStoper _gameTimeStoper;

        public Action GameResumeAction;
        
        public void Construct(GameObject resetMenu, ISceneLoader sceneLoader, GameObject bootstraper)
        {
            _bootstraper = bootstraper;
            _sceneLoader = sceneLoader;
            _resetMenu = resetMenu;
            UIDeathPopupProvider resetPopup = resetMenu.GetComponent<UIDeathPopupProvider>();
            resetPopup.Construct(this);
            _resetMenu.SetActive(false);
        }

        public void ShowResetMenu()
        {
            _resetMenu.SetActive(true);
        }

        public void ReloadGameButtonAction()
        {
            DataSaver dataSaver = (DataSaver)ServiceLocator.Instance.GetData(typeof(DataSaver));
            dataSaver.SaveResult();
        }

        public void MainMenu()
        {
            DataSaver dataSaver = (DataSaver)ServiceLocator.Instance.GetData(typeof(DataSaver));
            dataSaver.SaveResult();
            dataSaver.ResetResult();

            _gameTimeStoper.ResumeTime();
            _sceneLoader.LoadWithFinish(Constants.MAIN_MENU_SCENE, _bootstraper);
        }

        public void ResumeAll()
        {
            _resetMenu.SetActive(false);
            GameResumeAction?.Invoke();
        }

        public void AddTimeStoper(GameTimeStoper gameTimeStoper)
        {
            _gameTimeStoper = gameTimeStoper;
        }
    }
}