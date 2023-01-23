using System;
using UnityEngine;

namespace OutFoxeed.UsefulStructs
{
    [System.Serializable]
    public abstract class Cooldown
    {
        [SerializeField] float duration;
        public float Duration => duration;

        protected float value;

        protected Cooldown(float duration, bool setCooldownReady = false)
        {
            SetDuration(duration, !setCooldownReady);
        }

        public abstract bool IsReady();
        public abstract void SetReady();

        public void SetDuration(float newDuration, bool resetCooldown = false)
        {
            duration = newDuration;
            if (resetCooldown) Reset();
        }

        public abstract void Reset();
    }
}