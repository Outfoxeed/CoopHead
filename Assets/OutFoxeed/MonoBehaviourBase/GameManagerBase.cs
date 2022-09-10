using Mono.Cecil.Cil;
using UnityEngine;

namespace OutFoxeed.MonoBehaviourBase
{
    public class GameManagerBase<T> : SingletonBase<T> where T : MonoBehaviour
    {
        // Have a cleaner reference to Camera.main
        private Camera mainCam;
        public Camera MainCam => mainCam;

        // Pause system //TODO: remove static vars
        private bool paused;
        public bool IsPaused => paused;
        private float wantedTimeScale;
        public void SetPause(bool pause)
        {
            if (paused == pause)
                return;
            paused = pause;
            
            if (pause) {wantedTimeScale = Time.timeScale;}
            Time.timeScale = paused ? 0f : wantedTimeScale;
            
            OnSetPause();
        }
        protected virtual void OnSetPause()
        {
            
        }

        protected override void Awake()
        {
            base.Awake();

            mainCam = Camera.main;
            wantedTimeScale = 1f;
        }
    }
}