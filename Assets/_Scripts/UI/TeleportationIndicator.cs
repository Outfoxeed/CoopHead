using System;
using System.Collections;
using UnityEngine;

namespace CoopHead.UI
{
    public class TeleportationIndicator : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text counterText;
        private float remaining;
        
        private void Start()
        {
            Player.instance.onLapEnd += (float duration) =>
            {
                gameObject.SetActive(true);
                remaining = duration;
            };
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (remaining <= 0)
            {
                gameObject.SetActive(false);
                return;
            }

            counterText.text = remaining.ToString("F1");
            remaining -= Time.deltaTime;
        }
    }
}