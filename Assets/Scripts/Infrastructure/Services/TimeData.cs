namespace Infrastructure.Services
{
    public class TimeData
    {
        public float BetweenWaveTime { get; private set; }
        public float StartedTime { get; private set; }

        public TimeData(int betweenWaveBetweenWaveTime, int staredTime)
        {
            BetweenWaveTime = betweenWaveBetweenWaveTime;
            StartedTime = staredTime;
        }
    }
}