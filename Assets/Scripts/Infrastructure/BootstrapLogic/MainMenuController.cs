using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private GameObject _panelCreators;
    private GameObject _panelSound;
    private GameObject _panelHelp;
    private GameObject _fadeObject;
    
    public void Init(Transform modal, Transform panelSound, Transform panelHelp, Transform panelCreators, PanelResourceProvider provider)
    {
        _fadeObject = modal.gameObject;
        _panelSound = panelSound.gameObject;   
        _panelHelp = panelHelp.gameObject;
        _panelCreators = panelCreators.gameObject;


        provider.AddMethods(ClosePanelCredits ,ClosePanelSound,ClosePanelHelp);
    }
    
    
    public void OpenPanelCredits()
    {
        _panelCreators.SetActive(true);
        ShowFadeObject();
    }

    public void ClosePanelCredits()
    {
        _panelCreators.SetActive(false);
        HideFadeObject();
    }

    public void OpenPanelSound()
    {
        _panelSound.SetActive(true);
        ShowFadeObject();
    }

    public void ClosePanelSound()
    {
        _panelSound.SetActive(false);
        HideFadeObject();
    }

    public void OpenPanelHelp()
    {
        _panelHelp.SetActive(true);
        ShowFadeObject();
    }

    public void ClosePanelHelp()
    {
        _panelHelp.SetActive(false);
        HideFadeObject();
    }

    private void ShowFadeObject()
    {
        _fadeObject.SetActive(true);
    }

    private void HideFadeObject()
    {
        _fadeObject.SetActive(false);
    }
}
