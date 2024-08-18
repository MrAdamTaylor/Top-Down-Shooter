using UnityEngine;

public class LoadLevelState : IPayloadedState<string>
{
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    
    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
    {
        _stateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _loadingCurtain = loadingCurtain;
    }
    
    public void Enter(string sceneName)
    {
        Debug.Log("Переход в состояние загрузки уровня");
        _sceneLoader.Load(sceneName, onLoaded);
    }

    public void onLoaded()
    {
        Debug.Log("Выполняем действия на новой сцене");
    }

    public void Exit()
    {
        Debug.Log("Выход из состояния загрузки уровня");
    }
}