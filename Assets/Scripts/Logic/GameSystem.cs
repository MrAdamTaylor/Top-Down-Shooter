using System;
using Infrastructure.ServiceLocator;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Logic
{
    public class GameSystem : MonoBehaviour
    {
        private GameObject _resetMenu;

        private GameObject _bootstraper;
        private GameObject _loadCurtain;

        public Action GameResumeAction;
        
        public void Construct(GameObject resetMenu, GameObject bootstraper, GameObject loadCurtain)
        {
            _bootstraper = bootstraper;
            _loadCurtain = loadCurtain;
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
            Destroy(_bootstraper);
            Destroy(_loadCurtain);
            SceneManager.LoadScene(0);
        }

        public void ResumeAll()
        {
            _resetMenu.SetActive(false);
            GameResumeAction?.Invoke();
        }
    }
}