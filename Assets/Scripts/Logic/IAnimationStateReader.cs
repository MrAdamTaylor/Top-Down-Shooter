public interface IAnimationStateReader
{
    void EnteredState(int stateInfoShortNameHash);
    void ExitedState(int stateInfoShortNameHash);

    AnimatorState State { get; }
}