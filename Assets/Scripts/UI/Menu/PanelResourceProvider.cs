using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelResourceProvider : MonoBehaviour
{
    [SerializeField] private Button _buttonCreatorsClose;
    [SerializeField] private Button _buttonSettingsClose;
    [SerializeField] private Button _buttonHelpClose;

    
    private Action _onCloseCredits, _onCloseSound, _onCloseHelp;
    
    public void AddMethods(Action closeCredits, Action closeSound, Action closeHelp)
    {
        _onCloseCredits = closeCredits;
        _onCloseSound = closeSound;
        _onCloseHelp = closeHelp;
        
        _buttonCreatorsClose.onClick.AddListener(()=> _onCloseCredits());
        _buttonSettingsClose.onClick.AddListener(()=> _onCloseSound());
        _buttonHelpClose.onClick.AddListener(()=> _onCloseHelp());
    }

    public void OnDestroy()
    {
        _buttonCreatorsClose.onClick.RemoveAllListeners();
        _buttonSettingsClose.onClick.RemoveAllListeners();
        _buttonHelpClose.onClick.RemoveAllListeners();
    }
}
