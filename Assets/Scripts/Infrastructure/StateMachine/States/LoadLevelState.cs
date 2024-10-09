using EnterpriceLogic.Constants;
using Infrastructure.Services.AssertService.ExtendetAssertService;
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

        #region Temp Assert Test

        SpawnerTestAssert testAssert = (SpawnerTestAssert)ServiceLocator.Instance.GetData(typeof(SpawnerTestAssert));
        
        AssertServiceString<ParticleSystem> particleAssert =(AssertServiceString<ParticleSystem>)ServiceLocator
            .Instance.GetData(typeof(IAssertByString<ParticleSystem>));
        
        AssertServiceString<LineRenderer> lineRendererAssert = (AssertServiceString<LineRenderer>)ServiceLocator
            .Instance.GetData(typeof(IAssertByString<LineRenderer>));
        
        AssertServiceString<TrailRenderer> trailRendererAssert = (AssertServiceString<TrailRenderer>)ServiceLocator
            .Instance.GetData(typeof(IAssertByString<TrailRenderer>));
        
        AssertServiceString<GameObject> objAssert = (AssertServiceString<GameObject>)ServiceLocator
            .Instance.GetData(typeof(IAssertByString<GameObject>));

        particleAssert.Assert(testAssert.PathToParticle);
        lineRendererAssert.Assert(testAssert.PathToLine);
        trailRendererAssert.Assert(testAssert.PathToTrail);
        objAssert.Assert(testAssert.PathToObject);

        #endregion
        
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