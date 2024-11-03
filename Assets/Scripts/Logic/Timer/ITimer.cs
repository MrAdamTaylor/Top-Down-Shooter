namespace Logic.Timer
{
    public interface ITimer
    {
        public bool IsActive { get; }

        public void StartTimer();
        public void ReloadTimer(float Time);
        public void PauseResume();
        public void StopTimer();
    }
}