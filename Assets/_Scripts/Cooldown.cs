using UnityEngine;

namespace CoopHead
{
    [System.Serializable]
    public class Cooldown
    {
        private float remaining;
        [SerializeField]
        private float duration;
        public float Duration => duration;
        
        public System.Action onReady;

        public Cooldown(float duration) : this(duration, null)
        {
            
        }
        public Cooldown(float duration, System.Action onReady)
        {
            this.duration = duration;
            Reset();
            this.onReady = onReady;
        }
        
        public bool IsReady => remaining <= 0;
        public void Decrease(float amount)
        {
            if (IsReady)
                return;
            remaining -= amount;
            if(IsReady) onReady?.Invoke();
        }

        public void SetDuration(float newDuration, bool resetCooldown = false)
        {
            duration = newDuration;
            if(resetCooldown) Reset();
        }

        public void Reset() => remaining = duration;

    }
}