namespace Logic.Timer
{
    public interface ITimer
    {
        public void StartTimer();
        public void ReloadTimer(float Time);
        public void PauseResume();
        public void StopTimer();
    }
}