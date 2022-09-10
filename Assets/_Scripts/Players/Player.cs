using OutFoxeed.MonoBehaviourBase;
using UnityEngine;

namespace CoopHead
{
    [RequireComponent(typeof(PlayerController))]
    public partial class Player : SingletonBase<Player>
    {
        private PlayerController playerController;

        private int currentRoomIndex;
        public int CurrentRoomIndex => currentRoomIndex;
        public System.Action<int> onRoomChanged;

        private Checkpoint currentCheckpoint;

        protected override void Awake()
        {
            base.Awake();
            playerController = GetComponent<PlayerController>();
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
            }
        }

        private void OnRoomTouched(Room roomTouched)
        {
            var newRoomIndex = RoomsManager.instance.GetRoomIndex(roomTouched);
            if (currentRoomIndex == newRoomIndex)
                return;
            currentRoomIndex = newRoomIndex;
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
            transform.position = currentCheckpoint.transform.position;
        }

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
    }
}