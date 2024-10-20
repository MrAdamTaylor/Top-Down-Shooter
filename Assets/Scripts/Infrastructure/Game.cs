public class Game
{
    public GameStateMachine StateMachine;
    
    public Game(LoadingCurtain curtain, LevelConfigs levelConfigs)
    {
        StateMachine = new GameStateMachine(new SceneLoader(), curtain, AllServices.Container,levelConfigs);
    }
}