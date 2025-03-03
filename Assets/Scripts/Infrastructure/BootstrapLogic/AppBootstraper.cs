using UnityEngine;

namespace Infrastructure.BootstrapLogic
{
    public class AppBootstraper : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            var obj = Instantiate(Resources.Load(BootConstants.PATH_TO_BOOT)) as GameObject;
            obj.name = obj.name.Replace("(Clone)", string.Empty);
            DontDestroyOnLoad(obj);
            CreateDiContainer();
            CreateUI();
        }

        private static void CreateDiContainer()
        {
            
        }

        private static void CreateUI()
        {
            GameObject uiController = new GameObject(BootConstants.UI_CONTROLLER_NAME);
            uiController.AddComponent<MainMenuController>();
            MainMenuConfigurator configurator = uiController.AddComponent<MainMenuConfigurator>();
            
            GameObject mainMenuCanvasLoaded = Resources.Load<GameObject>(BootConstants.PATH_TO_MAIN_CANVAS) ;
            GameObject additionalMenuCanvasLoaded = Resources.Load<GameObject>(BootConstants.PATH_TO_ADDITIONAL_CANVAS);
            
            GameObject mainMenuResource = Instantiate(mainMenuCanvasLoaded);
            GameObject additionalMenuResource = Instantiate(additionalMenuCanvasLoaded);
            
            
            mainMenuResource.transform.SetParent(uiController.transform, false);
            additionalMenuResource.transform.SetParent(uiController.transform, false);
            
            configurator.Init(mainMenuResource, additionalMenuResource);
        }
    }
}
