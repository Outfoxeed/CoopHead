using System;
using OutFoxeed.MonoBehaviourBase;
using UnityEngine;

namespace CoopHead
{
    public class ScoreManager : SingletonBase<ScoreManager>
    {
        private GameManager gm;
        
        private float score;
        public float Score
        {
            get => score;
            private set
            {
                if (value < score)
                    return;
                score = value;
                OnScoreUpdated?.Invoke(score);
            }
        }
        public System.Action<float> OnScoreUpdated;

        private bool scoreSaved;
        private string bestScorePrefName = "Bestscore";

        private void Start()
        {
            gm = GameManager.instance;
            gm.onGameEnd += SaveScore;
            Score = 0;
        }
        
        private void Update()
        {
            if (gm.CurrentGameState != GameManager.GameState.Game)
                return;

            Score += Time.deltaTime;
        }

        public void SaveScore()
        {
            if (scoreSaved)
                return;
            scoreSaved = true;

            float bestScore = GetBestScore();
            Debug.Log($"Get best score => {bestScore}");
            if (bestScore > score) PlayerPrefs.SetFloat(bestScorePrefName, score);
        }
        public float GetBestScore() => PlayerPrefs.GetFloat(bestScorePrefName, 60*60-1);

        private string FormatScore(long toFormat)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(toFormat);
            return $"{timeSpan.Minutes:D}:{timeSpan.Seconds:D2}";
        }

        public string BestScoreFormatted() => FormatScore((long) GetBestScore());
        public string ScoreFormatted() => FormatScore((long)Score);
    }
}