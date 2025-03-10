using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

public class MainMenuConfigurator : MonoBehaviour
{
    private static class ButtonsName
    {
        public const string MODAL = "BackgroundFade";
        public const string PANEL_SOUND = "PanelSound";
        public const string PANEL_HELP = "PanelHelp";
        public const string PANEL_CREATORS = "PanelCreators";
    }

    private GameObject _panelSound;
    private GameObject _panelHelp;
    private GameObject _panelCreaters;
    private GameObject _gameBootstraper;
    
    
    public void Init(GameObject mainMenuCanvas, GameObject additionalMenuCanvasLoaded, MainMenuController menuController)
    {

        #region LoadPanels
        
        Transform modal = FindDeepChild(additionalMenuCanvasLoaded.transform, ButtonsName.MODAL);
        Transform panelSound =  FindDeepChild(additionalMenuCanvasLoaded.transform, ButtonsName.PANEL_SOUND);
        Transform panelHelp =  FindDeepChild(additionalMenuCanvasLoaded.transform, ButtonsName.PANEL_HELP);
        Transform panelCreators =  FindDeepChild(additionalMenuCanvasLoaded.transform, ButtonsName.PANEL_CREATORS);

        PanelResourceProvider provider = additionalMenuCanvasLoaded.GetComponentInChildren<PanelResourceProvider>();
        
        #endregion
        
        
        
        menuController.Init(modal, panelSound, panelHelp, panelCreators, provider);
        
        Debug.Log($"Loaded transform:  modal: {modal.gameObject.name}, panelSound {panelSound.gameObject.name}, " +
                  $"panelHelp {panelHelp.gameObject.name}, panelCreators {panelCreators.gameObject.name}");
    }
    

    Transform FindDeepChild(Transform parent, string componentName)
    {
        foreach (Transform child in parent.GetComponentsInChildren<Transform>(true)) 
        {
            if (child.gameObject.name != componentName) continue;
            
            return child;
        }
        return null; 
    }
}
