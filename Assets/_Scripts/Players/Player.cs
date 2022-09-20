using System.Collections;
using System.Collections.Generic;
using OutFoxeed.MonoBehaviourBase;
using UnityEngine;

namespace CoopHead
{
    [RequireComponent(typeof(PlayerController))]
    public partial class Player : SingletonBase<Player>
    {
        private Rigidbody2D rb;
        private PlayerController playerController;
        private int currentRoomIndex;
        public int CurrentRoomIndex => currentRoomIndex;
        public System.Action<int> onRoomChanged;

        [Header("Laps"), SerializeField]
        private int lapCountMax = 2;
        private int lapCount;
        private bool lapEndReached;
        [SerializeField] private float timeBeforeEndLapTp = 10f;
        public System.Action<float> onLapEnd;

        private Checkpoint currentCheckpoint;

        protected override void Awake()
        {
            base.Awake();
            playerController = GetComponent<PlayerController>();
            lapCount = 0;
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            switch (other.tag)
            {
                case "Death":
                    Die();
                    break;
                case "Checkpoint":
                    OnCheckpointTouched(other.gameObject);
                    break;
                case "Room":
                    if (other.gameObject.TryGetComponent(out Room room))
                        OnRoomTouched(room);
                    break;
                case "End":
                    if (lapEndReached)
                        break;
                    lapEndReached = true;
                    lapCount++;
                    if (lapCount == lapCountMax)
                    {
                        // Last lap End
                        GameManager.instance.CurrentGameState = GameManager.GameState.End;
                    }
                    else
                    {
                        //
                        StartCoroutine(OnFirstLapEnd());
                        onLapEnd?.Invoke(timeBeforeEndLapTp);
                    }
                    break;
            }
        }

        IEnumerator OnFirstLapEnd()
        {
            yield return new WaitForSeconds(timeBeforeEndLapTp);
            lapEndReached = false;

            ChangeCurrentRoom(0);
            currentCheckpoint = RoomsManager.instance.checkpoints[0];
            
            transform.position = currentCheckpoint.transform.position;
        } 

        private void OnRoomTouched(Room roomTouched)
        {
            var newRoomIndex = RoomsManager.instance.GetRoomIndex(roomTouched);
            if (currentRoomIndex == newRoomIndex)
                return;
            ChangeCurrentRoom(newRoomIndex);
        }

        void ChangeCurrentRoom(int index)
        {
            currentRoomIndex = index;
            onRoomChanged?.Invoke(currentRoomIndex);
        }

        private void Die()
        {
            playerController.OnDeath();

            // Gain all cooldowns
            lowBoostPlaceCooldown.SetReady();
            strongBoostPlaceCooldown.SetReady();
            strongBoostUseCooldown.SetReady();

            // Stop super boost coroutine if needed
            if (strongBoostCoroutine != null)
            {
                StopCoroutine(strongBoostCoroutine);
                strongBoostCoroutine = null;
            }

            // Tp player to checkpoint
            rb.position = currentCheckpoint.transform.position;
        }

        #region Checkpoints
        private void OnCheckpointTouched(GameObject checkpointGo)
        {
            if (!checkpointGo.TryGetComponent(out Checkpoint checkpoint))
                return;

            if (currentCheckpoint == checkpoint)
                return;

            // Don't take new checkpoint if it is before the current one
            if (!RoomsManager.instance.IsCheckpointSuperior(checkpoint, currentCheckpoint))
                return;

            SetCurrentCheckpoint(checkpoint);
        }

        private void SetCurrentCheckpoint(Checkpoint checkpoint)
        {
            currentCheckpoint = checkpoint;
        }
        #endregion
    }
}