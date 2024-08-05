using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class GameCyrcleManager : MonoBehaviour
{
    [ReadOnly]
    [SerializeField]
    private GameState _gameStatr = GameState.Off;

    public void OnStart()
    {
    }

}

public class Listeners 
{
    public interface IGameListener
    {
    }


    public interface IGameStartListener : IGameListener
    {
        void OnStart();
    }

    public interface IGameFinishListener : IGameListener
    {
        void OnFinish();
    }

    public interface IGamePauseListener : IGameListener
    {
        void OnPause();
    }

    public interface IGameResumeListener : IGameListener
    {
        void OnResume();
    }

    public interface IGameUpdateListener : IGameListener
    {
        void OnUpdate(float timeDelta);
    }

    public interface IGameFixedUpdateListener : IGameListener
    {
        void FixedUpdate(float timeDelta);
    }

    public interface IGameLateUpdateListener : IGameListener
    {
        void OnLateUpdate(float timeDelta);
    }
}
