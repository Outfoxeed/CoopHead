using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace CoopHead.UI
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text highscoreText;

        [Header("Restart option")] [SerializeField]
        private float _delayDuration = 1f;
        private float _delay;
        [SerializeField] private GameObject _restartMessage;
        private bool _waitingForInput;

        private void Start()
        {
            GameManager.instance.onGameEnd += Activate;
            _restartMessage.SetActive(false);
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (!_waitingForInput)
            {
                return;
            }
            
            if (GameManager.instance.RewiredPlayer.GetButton("Jump"))
            {
                GameManager.instance.RestartGame();
            }
        }

        private async Task WaitForDuration(float delay, Action callback)
        {
            await Task.Delay(TimeSpan.FromSeconds(delay));
            callback?.Invoke();
        }

        private void Activate()
        {
            gameObject.SetActive(true);
            var scoreManager = ScoreManager.instance;
            scoreManager.SaveBestScore();
            scoreText.text = scoreManager.ScoreFormatted();
            highscoreText.text = string.Concat(scoreManager.GetBestScore().ToString("F1"), " seconds");
            
            _delay = 0f;
            WaitForDuration(_delayDuration, ShowRestartMessageAndWaitForInput);
            Debug.Log($"Score = {scoreManager.Score} and Highscore = {scoreManager.GetBestScore()}");
        }
        private void ShowRestartMessageAndWaitForInput()
        {
            _restartMessage.SetActive(true);
            _waitingForInput = true;
        }
    }
}