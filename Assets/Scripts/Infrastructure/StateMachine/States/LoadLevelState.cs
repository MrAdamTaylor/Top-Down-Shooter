using Unity.VisualScripting;
using UnityEngine;

public class LoadLevelState : IPayloadedState<string>
{
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IGameFactory _gameFactory;
    
    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
    IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
        _stateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _loadingCurtain = loadingCurtain;
    }
    
    public void Enter(string sceneName)
    {
        Debug.Log("Переход в состояние загрузки уровня");
        _sceneLoader.Load(sceneName, OnLoaded);
        /*var obj = GameObject.FindObjectOfType(typeof(UIInstaller));
        UIInstaller installer = (UIInstaller)obj;
        installer.Execute();*/
    }

    private void OnLoaded()
    {
        Debug.Log("Выполняем действия на новой сцене");

        Camera camera = GameObject.FindObjectOfType<Camera>();
        GameObject startPosition = GameObject.FindGameObjectWithTag(Constants.INITIAL_POSITION);
        Debug.Log("Start Position is "+startPosition.transform.position + "GameObj name "+startPosition.name);
        GameObject player = _gameFactory.CreatePlayer(startPosition.transform.position, camera);

        /*var obj = GameObject.FindObjectOfType(typeof(UIInstaller));
        UIInstaller installer = (UIInstaller)obj;
        installer.Execute();*/
    }

    public void Exit()
    {
        Debug.Log("Выход из состояния загрузки уровня");
    }
}