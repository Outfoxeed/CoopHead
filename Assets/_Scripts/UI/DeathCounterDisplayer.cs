using System;
using UnityEngine;

namespace CoopHead.UI
{
    public class DeathCounterDisplayer : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text text;
        private string startText;
        
        private void Start()
        {
            startText = text.text;
            UpdateDisplay(0);
            Player.instance.OnDeath += UpdateDisplay;
        }

        private void UpdateDisplay(int deathCount)
        {
            text.text = startText + " " + deathCount.ToString();
        }
    }
}