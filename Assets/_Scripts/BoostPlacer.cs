using UnityEngine;

namespace CoopHead
{
    public class BoostPlacer : MonoBehaviour
    {
        [SerializeField]
        private Transform boostPrefab;
        [SerializeField] private float boostDestroyTime;

        private Rewired.Player rewiredPlayer;
        private Camera camera;

        [SerializeField]
        private Cooldown placeCooldown;

        private void Start()
        {
            rewiredPlayer = GameManager.Instance.RewiredPlayer;
            camera = GameManager.Instance.MainCam;
        }

        private void Update()
        {
            placeCooldown.Decrease(Time.deltaTime);
            
            if (rewiredPlayer.GetButtonDown("CreateObject"))
            {
                if (placeCooldown.IsReady)
                {
                    Vector2 worldPos = camera.ScreenToWorldPoint(Input.mousePosition);
                    CreateObject(worldPos);
                    placeCooldown.Reset();
                }
            }
        }

        private void CreateObject(Vector2 worldPos)
        {
            Destroy(Instantiate(boostPrefab, worldPos, Quaternion.identity).gameObject, boostDestroyTime);
        }
    }
}