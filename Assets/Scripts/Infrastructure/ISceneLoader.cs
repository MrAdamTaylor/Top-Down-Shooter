using System;

namespace Infrastructure
{
    public interface ISceneLoader
    {
        public void Construct(string sceneName);

        public void Load(string gameScene);
        public void Load(string gameScene, Action onLoaded);
    }
}