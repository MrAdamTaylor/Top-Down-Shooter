using System.Collections.Generic;
using System.Linq;
using EnterpriceLogic.Constants;
using Infrastructure.Services.AssertService.ExtendetAssertService;
using UnityEngine;

public class BootstrapState : IState
{
    private GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;
    private string _level;
    
    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services, LevelConfigs levelConfigs)
    {
        _services = services;
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        RegisterServices(levelConfigs.SpawnerConfigsList);
        ServiceLocator.Instance.BindData(typeof(LevelConfigs), levelConfigs);
        ServiceLocator.Instance.BindData(typeof(PlayerConfigs), levelConfigs.PlayerConfigs);
        _level = levelConfigs.LevelName;
    }

    public void Enter()
    {
        _sceneLoader.Load(_level, onLoaded: EnterLoadLevel);
    }

    public void Exit()
    {
    }

    private void RegisterServices(List<SpawnerConfigs> spawnerConfigsEnumerable)
    {
        #region Register New AssertServices
        ServiceLocator.Instance.BindData(typeof(IAssertByString<LineRenderer>), new AssertServiceString<LineRenderer>());
        ServiceLocator.Instance.BindData(typeof(IAssertByString<TrailRenderer>), new AssertServiceString<TrailRenderer>());
        ServiceLocator.Instance.BindData(typeof(IAssertByObj<GameObject>), new AssertServiceObj<GameObject>());
        
        AssertServiceString<LineRenderer> lineRendererAssert = (AssertServiceString<LineRenderer>)ServiceLocator
            .Instance.GetData(typeof(IAssertByString<LineRenderer>));
        AssertServiceString<TrailRenderer> trailRendererAssert = (AssertServiceString<TrailRenderer>)ServiceLocator
            .Instance.GetData(typeof(IAssertByString<TrailRenderer>));
        AssertServiceObj<GameObject> gameObj = (AssertServiceObj<GameObject>)ServiceLocator
            .Instance.GetData(typeof(IAssertByObj<GameObject>));
        
        #endregion
        
        _services.RegisterSingle<IAsserts>(new Asserts());
        _services.RegisterSingle<ISpecialEffectFactory>
            (new SpecialEffectFactory(trailRendererAssert,lineRendererAssert));
        
        _services.RegisterSingle<IWeaponFactory>(new WeaponFactory(_services.Single<IAsserts>()));
        _services.RegisterSingle<IPlayerFactory>(new PlayerFactory(_services.Single<IAsserts>(), 
            _services.Single<IWeaponFactory>()));
        _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IAsserts>()));
        _services.RegisterSingle<IEnemyFactory>(new EnemyFactory(gameObj));
        ServiceLocator.Instance.BindData(typeof(ISpecialEffectFactory),_services.Single<ISpecialEffectFactory>());
        ServiceLocator.Instance.BindData(typeof(UIAnimationPlayer), new UIAnimationPlayer());

        #region Temp Assert Test
        SpawnerConfigs forAssert = spawnerConfigsEnumerable[0];
        SpawnerTestAssert testAssert = (SpawnerTestAssert)forAssert;
        ServiceLocator.Instance.BindData(typeof(SpawnerTestAssert), testAssert);
        ServiceLocator.Instance.BindData(typeof(IAssertByString<ParticleSystem>), new AssertServiceString<ParticleSystem>());
        ServiceLocator.Instance.BindData(typeof(IAssertByString<GameObject>), new AssertServiceString<GameObject>());
        var particleAssert = ServiceLocator.Instance.GetData(typeof(IAssertByString<ParticleSystem>));
        var gameObjectAssert = ServiceLocator.Instance.GetData(typeof(IAssertByString<GameObject>));
        #endregion
    }

    private void EnterLoadLevel() => 
        _stateMachine.Enter<LoadLevelState, string>(_level);
}