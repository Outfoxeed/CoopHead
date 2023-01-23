namespace OutFoxeed.UsefulStructs
{
    [System.Serializable]
    public class ManualCooldown : Cooldown
    {
        private System.Action onReady;

        public ManualCooldown(float duration, System.Action onReady, bool setCooldownReady = false) : base(duration,
            setCooldownReady)
        {
            this.onReady = onReady;
        }

        public override bool IsReady() => value <= 0f;
        public override void SetReady() => value = 0f;
        public override void Reset() => value = Duration;

        public void Decrease(float deltaTime)
        {
            if (value <= 0)
                return;

            value -= deltaTime;
            if (value <= 0)
            {
                onReady?.Invoke();
            }
        }
    }
}