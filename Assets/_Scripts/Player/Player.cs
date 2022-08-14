using System;
using System.Collections;
using System.Collections.Generic;
using CoopHead;
using UnityEngine;

public class Player : SingletonBase<Player>
{
    private int currentRoomIndex;
    public int CurrentRoomIndex => currentRoomIndex;
    public System.Action<int> onRoomChanged;

    private Checkpoint currentCheckpoint;
    
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
                OnRoomTouched(other.gameObject);
                break;
        }
    }

    private void OnRoomTouched(GameObject roomTouched)
    {
        var newRoomIndex = RoomsManager.Instance.GetRoomIndex(roomTouched);
        if (currentRoomIndex == newRoomIndex)
            return;
        currentRoomIndex = newRoomIndex;
        onRoomChanged?.Invoke(currentRoomIndex);
    }

    private void Die()
    {
        transform.position = currentCheckpoint.transform.position;
    }

    private void OnCheckpointTouched(GameObject checkpointGo)
    {
        if (!checkpointGo.TryGetComponent(out Checkpoint checkpoint))
            return;

        if (currentCheckpoint == checkpoint)
            return;

        // Don't take new checkpoint if it is before the current one
        if (!RoomsManager.Instance.IsCheckpointSuperior(checkpoint, currentCheckpoint))
            return;

        SetCurrentCheckpoint(checkpoint);
    }
    private void SetCurrentCheckpoint(Checkpoint checkpoint)
    {
        currentCheckpoint = checkpoint;
    }
}
