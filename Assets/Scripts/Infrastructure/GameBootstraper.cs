using Configs;
using Infrastructure.StateMachine.States;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstraper : MonoBehaviour
    {
        [SerializeField] private LevelConfigs _levelConfigs;
        [SerializeField] private AsyncSceneLoader _sceneLoader;
        private Game _game;
        

        public void StartGame()
        {
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(GameBootstraper), this);
            _game = new Game(_sceneLoader, _levelConfigs);
            _game.StateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}

