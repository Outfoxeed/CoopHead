using TMPro;
using UnityEngine;

namespace CoopHead.UI
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text highscoreText;

        private void Start()
        {
            GameManager.instance.onGameEnd += Activate;
            gameObject.SetActive(false);
        }

        private void Activate()
        {
            gameObject.SetActive(true);
            var scoreManager = ScoreManager.instance;
            scoreText.text = scoreManager.Score.ToString("F1");
            highscoreText.text = scoreManager.GetHighScore().ToString("F1");
            
            Debug.Log($"Score = {scoreManager.Score} and Highscore = {scoreManager.GetHighScore()}");
        }
    }
}