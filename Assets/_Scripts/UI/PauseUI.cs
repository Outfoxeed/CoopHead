using System;
using System.Collections;
using System.Collections.Generic;
using CoopHead;
using TMPro;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _highscoreText;

    private void OnEnable()
    {
        if (ScoreManager.instance == null)
        {
            return;
        }
        _highscoreText.text = ScoreManager.instance.GetBestScore().ToString("F1");
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }
}
