using EnterpriceLogic.Constants;
using UnityEngine;

public class LoadLevelState : IPayloadedState<string>
{
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IPlayerFactory _playerFactory;
    private readonly IUIFactory _uiFactory;
    
    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
    IPlayerFactory playerFactory, IUIFactory uiFactory)
    {
        _playerFactory = playerFactory;
        _stateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _loadingCurtain = loadingCurtain;
        _uiFactory = uiFactory;
    }
    
    public void Enter(string sceneName)
    {
        var canvas = _loadingCurtain.GetComponent<Canvas>();
        canvas.enabled = true;
        _loadingCurtain.Show();
        _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit()
    {
        _loadingCurtain.Hide();
    }

    private void OnLoaded()
    {
        Camera camera = Object.FindObjectOfType<Camera>();
        GameObject startPosition = GameObject.FindGameObjectWithTag(Constants.INITIAL_POSITION);
        GameObject player = _playerFactory.Create(startPosition.transform.position, camera);
        GameObject canvas = GameObject.FindGameObjectWithTag("PlayerUI");
        GameObject ui = _uiFactory.CreateWithLoadConnect(PrefabPath.UI_PLAYER_PATH, canvas, player);
        ConstructUI(ui);
        
        _stateMachine.Enter<GameLoopState>();
    }

    private void ConstructUI(GameObject ui)
    {
        GameObject warning = GameObject.FindGameObjectWithTag("Warning");
        warning.SetActive(false);
        UIHelper helper = ui.AddComponent<UIHelper>();
        helper.Construct();
    }
}