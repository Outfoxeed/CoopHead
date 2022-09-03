using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CoopHead.UI
{
    public class ScoreDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        private void Start()
        {
            UpdateDisplay(0);
            ScoreManager.instance.OnScoreUpdated += UpdateDisplay;
        }

        private void UpdateDisplay(float newScore)
        {
            if (!text)
                return;

            TimeSpan timeSpan = TimeSpan.FromSeconds((long)newScore);
            text.text = $"{timeSpan.Minutes:D}:{timeSpan.Seconds:D2}";
        }
    }
}