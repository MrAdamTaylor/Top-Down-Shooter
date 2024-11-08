using System;
using UnityEngine;

namespace Infrastructure
{
    public interface ISceneLoader
    {
        public void Construct(string sceneName);

        public void Load(string gameScene);
        public void Load(string gameScene, Action onLoaded);
        void LoadWithFinish(string mainMenuScene, GameObject bootstraper);
    }
}