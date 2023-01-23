using System.Collections;
using OutFoxeed.MonoBehaviourBase;
using OutFoxeed.UsefulStructs;
using UnityEngine;
using UnityEngine.Serialization;

namespace CoopHead
{
    public partial class Player : SingletonBase<Player>
    {
        private Rewired.Player rewiredPlayer;
        private Camera camera;

        [Header("Low Boost")] [FormerlySerializedAs("boostPrefab"), SerializeField]
        private Transform lowBoostPrefab;

        [FormerlySerializedAs("boostDestroyTime"), SerializeField]
        private float lowBoostDestroyTime;

        [SerializeField] private ManualCooldown lowBoostPlaceCooldown;

        [Header("Strong Boost"), SerializeField]
        private Transform strongBoostPrefab;

        private Transform strongBoostInstance;
        private Transform strongBoostInstanceArrow;
        [SerializeField] private float strongBoostActivationRange;
        [SerializeField] private float strongBoostActivationDuration;
        [Space(5)] [SerializeField] private ManualCooldown strongBoostPlaceCooldown;
        [SerializeField] private ManualCooldown strongBoostUseCooldown;
        private bool CanMoveStrongBoost => strongBoostPlaceCooldown.IsReady() && strongBoostUseCooldown.IsReady();
        private IEnumerator strongBoostCoroutine;

        private void Start()
        {
            var gameManager = GameManager.instance;
            rewiredPlayer = gameManager.RewiredPlayer;
            camera = gameManager.MainCam;
        }

        private void Update()
        {
            lowBoostPlaceCooldown.Decrease(Time.deltaTime);
            strongBoostPlaceCooldown.Decrease(Time.deltaTime);
            strongBoostUseCooldown.Decrease(Time.deltaTime);

            if (rewiredPlayer.GetButtonDown("NormalBoost"))
            {
                if (lowBoostPlaceCooldown.IsReady())
                {
                    Vector2 worldPos = camera.ScreenToWorldPoint(Input.mousePosition);
                    CreateObject(worldPos);
                    lowBoostPlaceCooldown.Reset();
                }
            }
            else if (rewiredPlayer.GetButtonDown("StrongBoost"))
            {
                // If no instance existing, we instantiate the prefab
                if (!strongBoostInstance && strongBoostPlaceCooldown.IsReady())
                {
                    strongBoostPlaceCooldown.Reset();

                    Vector2 spawnPos = camera.ScreenToWorldPoint(Input.mousePosition);
                    strongBoostInstance = Instantiate(strongBoostPrefab, spawnPos, Quaternion.identity);
                    strongBoostInstanceArrow = strongBoostInstance.GetChild(strongBoostInstance.childCount - 1);
                }

                // If existing, we replace it
                else
                {
                    if (TryActivatingStrongBoost())
                    {
                        
                    }
                    else if (CanMoveStrongBoost)
                    {
                        strongBoostPlaceCooldown.Reset();

                        Vector2 newPos = camera.ScreenToWorldPoint(Input.mousePosition);
                        strongBoostInstance.position = newPos;
                        strongBoostInstance.rotation = Quaternion.identity;
                        strongBoostInstance.gameObject.SetActive(true);
                    }
                }
            }
            else if (rewiredPlayer.GetButton("StrongBoost"))
            {
                TryActivatingStrongBoost();
            }
        }

        private bool TryActivatingStrongBoost()
        {
            // If strong boost rady to use
            if (strongBoostInstance && strongBoostUseCooldown.IsReady())
            {
                // If player nearby activate the boost
                bool playerClose = Vector2.Distance(transform.position, strongBoostInstance.position) <
                                   strongBoostActivationRange;
                if (strongBoostInstance.gameObject.activeSelf && playerClose && strongBoostUseCooldown.IsReady())
                {
                    ActivateStrongBoost();
                    return true;
                }
            }

            return false;
        }

        private void ActivateStrongBoost()
        {
            // Activation
            strongBoostUseCooldown.Reset();
            if (strongBoostCoroutine != null)
                return;
            strongBoostCoroutine = UseStrongBoost();
            StartCoroutine(strongBoostCoroutine);
        }

        private void CreateObject(Vector2 worldPos)
        {
            Destroy(Instantiate(lowBoostPrefab, worldPos, Quaternion.identity).gameObject, lowBoostDestroyTime);
        }

        IEnumerator UseStrongBoost()
        {
            strongBoostInstanceArrow.gameObject.SetActive(true);
            playerController.BlockMovement(true);
            Vector2 launchDir = Vector2.up;
            yield return null;

            float timer = 0;
            while (timer < strongBoostActivationDuration)
            {
                launchDir = ((Vector2) camera.ScreenToWorldPoint(Input.mousePosition) - (Vector2) transform.position)
                    .normalized;
                strongBoostInstanceArrow.eulerAngles =
                    new Vector3(0, 0, Mathf.Atan2(launchDir.y, launchDir.x) * Mathf.Rad2Deg + 90f);
                if (rewiredPlayer.GetButtonDown("StrongBoost") || rewiredPlayer.GetButtonUp("StrongBoost"))
                {
                    break;
                }

                yield return null;
                timer += Time.deltaTime;
            }

            playerController.BlockMovement(false);
            playerController.StartSuperBoost(launchDir);

            strongBoostInstance.gameObject.SetActive(false);
            strongBoostInstanceArrow.gameObject.SetActive(false);
            strongBoostCoroutine = null;
        }
    }
}