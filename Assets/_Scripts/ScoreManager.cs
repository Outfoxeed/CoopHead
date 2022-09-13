﻿using OutFoxeed.MonoBehaviourBase;
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
                score = Mathf.Clamp(value, 0, Mathf.Infinity);
                OnScoreUpdated?.Invoke(score);
            }
        }
        public System.Action<float> OnScoreUpdated;

        private bool scoreSaved;

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

            float highscore = GetHighScore();
            if (highscore < score) PlayerPrefs.SetFloat("Highscore", score);
        }
        public float GetHighScore() => PlayerPrefs.GetFloat("Highscore", 0);
    }
}