using OutFoxeed.MonoBehaviourBase;
using UnityEngine;

namespace CoopHead
{
    public class ScoreManager : SingletonBase<ScoreManager>
    {
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

        public bool stopped;

        private void Start()
        {
            Score = 0;
        }
        
        private void Update()
        {
            if (GameManager.IsPaused || stopped)
                return;

            Score += Time.deltaTime;
        }

        public void SaveScore()
        {
            float highscore = PlayerPrefs.GetFloat("Highscore", 0);
            if (highscore < score) PlayerPrefs.SetFloat("Highscore", score);
        }
    }
}