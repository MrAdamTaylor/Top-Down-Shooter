using System;

namespace UI.MVC.Model
{
    public class ScoresStorage
    {
        public event Action<long> OnScoresChanged;
        
        public long Scores { get; private set; }
    
        public ScoresStorage(long ammo)
        {
            Scores = ammo;
        }

        public void AddScores(int current)
        {
            Scores += current;
            OnScoresChanged?.Invoke(Scores);
        }

        public void Reset()
        {
            Scores = 0;
            OnScoresChanged?.Invoke(Scores);
        }
    }
}