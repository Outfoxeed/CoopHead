using UnityEngine;

namespace OutFoxeed.MonoBehaviourBase
{
    public class GameManagerBase<T> : SingletonBase<T> where T : MonoBehaviour
    {
        // Have a cleaner reference to Camera.main
        private Camera mainCam;
        public Camera MainCam => mainCam;

        // Pause system
        private static bool paused;
        public static bool IsPaused => paused;
        private static float wantedTimeScale;
        public static void SetPause(bool pause)
        {
            paused = pause;
            if (pause) wantedTimeScale = Time.timeScale;
            else Time.timeScale = wantedTimeScale;
        }
        
        protected override void Awake()
        {
            base.Awake();

            mainCam = Camera.main;
            wantedTimeScale = 1f;
        }
    }
}