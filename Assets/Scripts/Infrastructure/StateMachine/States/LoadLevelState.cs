using UnityEngine;

public class LoadLevelState : IPayloadedState<string>
{
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IPlayerFactory _playerFactory;
    
    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
    IPlayerFactory playerFactory)
    {
        _playerFactory = playerFactory;
        _stateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _loadingCurtain = loadingCurtain;
    }
    
    public void Enter(string sceneName)
    {
        Debug.Log("Переход в состояние загрузки уровня");
        _sceneLoader.Load(sceneName, OnLoaded);
    }

    private void OnLoaded()
    {
        Debug.Log("Выполняем действия на новой сцене");
        GameObject warning = GameObject.FindGameObjectWithTag("Warning");
        warning.SetActive(false);
        Camera camera = GameObject.FindObjectOfType<Camera>();
        GameObject startPosition = GameObject.FindGameObjectWithTag(Constants.INITIAL_POSITION);
        GameObject player = _playerFactory.CreatePlayer(startPosition.transform.position, camera);
    }

    public void Exit()
    {
        Debug.Log("Выход из состояния загрузки уровня");
    }
}