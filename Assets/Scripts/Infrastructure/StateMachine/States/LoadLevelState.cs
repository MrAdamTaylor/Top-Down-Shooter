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
        _sceneLoader.Load(sceneName, OnLoaded);
    }

    private void OnLoaded()
    {
        Camera camera = Object.FindObjectOfType<Camera>();
        GameObject startPosition = GameObject.FindGameObjectWithTag(Constants.INITIAL_POSITION);
        GameObject player = _playerFactory.CreatePlayer(startPosition.transform.position, camera);
        GameObject canvas = GameObject.FindGameObjectWithTag("PlayerUI");
        GameObject ui = _uiFactory.CreateWithLoadConnect(Constants.UI_PLAYER_PATH, canvas, player);
        ConstructUI(ui);
    }

    public void ConstructUI(GameObject ui)
    {
        GameObject warning = GameObject.FindGameObjectWithTag("Warning");
        warning.SetActive(false);
        UIHelper helper = ui.AddComponent<UIHelper>();
        helper.Construct();
        //UIHelper helper = Object.FindObjectOfType<UIHelper>();
        //helper.gameObject.SetActive(true);
        //helper.Construct();
    }

    public void Exit()
    {
    }
}