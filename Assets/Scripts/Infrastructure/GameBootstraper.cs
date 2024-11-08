using Configs;
using Infrastructure.StateMachine.States;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstraper : MonoBehaviour
    {
        [SerializeField] private LevelConfigs _levelConfigs;
        [SerializeField] private LoadingCurtain _curtain;
        [SerializeField] private AsyncSceneLoader _sceneLoader;
        private Game _game;

        public void Awake()
        {
            var canvas = _curtain.GetComponent<Canvas>();
            canvas.enabled = false;
        }

        public void StartGame()
        {
            ServiceLocator.ServiceLocator.Instance.BindData(typeof(GameBootstraper), this);
            _game = new Game(_sceneLoader, _curtain, _levelConfigs);
            _game.StateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}

