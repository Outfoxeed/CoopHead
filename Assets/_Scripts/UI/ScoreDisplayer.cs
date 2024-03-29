﻿using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

namespace CoopHead.UI
{
    public class ScoreDisplayer : MonoBehaviour
    {
        [FormerlySerializedAs("text")] [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text bestScoreText;

        private ScoreManager sm;
        
        private void Start()
        {
            sm = ScoreManager.instance;
            sm.OnScoreUpdated += UpdateDisplay;
            UpdateDisplay(0);
        }

        private void UpdateDisplay(float newScore)
        {
            if (!scoreText)
                return;
            if(scoreText) scoreText.text = sm.ScoreFormatted();
            if(bestScoreText) bestScoreText.text = sm.BestScoreFormatted();
        }
    }
}