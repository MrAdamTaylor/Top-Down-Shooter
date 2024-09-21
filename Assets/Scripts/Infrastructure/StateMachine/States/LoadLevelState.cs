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
        _sceneLoader.Load(sceneName, OnLoaded);
    }

    private void OnLoaded()
    {
        GameObject warning = GameObject.FindGameObjectWithTag("Warning");
        warning.SetActive(false);
        Camera camera = Object.FindObjectOfType<Camera>();
        GameObject startPosition = GameObject.FindGameObjectWithTag(Constants.INITIAL_POSITION);
        GameObject player = _playerFactory.CreatePlayer(startPosition.transform.position, camera);
        ConstructUI(player);
    }

    public void ConstructUI(GameObject player)
    {
        GameObject ui = GameObject.FindGameObjectWithTag("PlayerUI");
        ui.gameObject.GetComponent<Canvas>().enabled = true;
        UIHelper helper = Object.FindObjectOfType<UIHelper>();
        ui.gameObject.SetActive(true);
        helper.gameObject.SetActive(true);
        var view = Object.FindObjectOfType<CurrencyProvider>();

        #region BindScores
        ServiceLocator.Instance.BindData(typeof(ScoresStorage), new ScoresStorage(Constants.STANDART_UI_VALUE));
        ServiceLocator.Instance.BindData(typeof(ScoresAdapter), new ScoresAdapter(
            view.ScoresView,
            (ScoresStorage)ServiceLocator.Instance.GetData(typeof(ScoresStorage))));
        ScoresAdapter scoresAdapter = (ScoresAdapter)ServiceLocator.Instance.GetData(typeof(ScoresAdapter));
        scoresAdapter.Initialize();
        #endregion

        #region BindMoney
        ServiceLocator.Instance.BindData(typeof(MoneyStorage), new MoneyStorage(Constants.STANDART_UI_VALUE));
        ServiceLocator.Instance.BindData(typeof(MoneyAdapter), new MoneyAdapter(
            view.MoneyView,
            (MoneyStorage)ServiceLocator.Instance.GetData(typeof(MoneyStorage))));
        MoneyAdapter moneyAdapter = (MoneyAdapter)ServiceLocator.Instance.GetData(typeof(MoneyAdapter));
        moneyAdapter.Initialize();
        helper.Construct();
        #endregion
        
        #region BindAmmo
        
        UIWeaponStaticDataIcons icons = Resources.Load<UIWeaponStaticDataIcons>(Constants.WEAPON_ICO_PATH);
        ServiceLocator.Instance.BindData(typeof(AmmoAdapter), new AmmoAdapter(view.AmmoView,icons, player.GetComponent<Scripts.Player.NewWeaponControllSystem.WeaponController>()));
        
        
        #endregion

    }

    public void Exit()
    {
    }
}