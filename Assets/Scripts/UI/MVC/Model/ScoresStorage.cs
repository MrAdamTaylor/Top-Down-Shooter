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
    
        public void AddScores(long current)
        {
            Scores += current;
            OnScoresChanged?.Invoke(Scores);
        }

        public void SpendScores(long current)
        {
            Scores -= current;
            OnScoresChanged?.Invoke(Scores);
        }
    }
}