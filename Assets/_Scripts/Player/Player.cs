using System;
using System.Collections;
using System.Collections.Generic;
using CoopHead;
using UnityEngine;

public class Player : SingletonBase<Player>
{
    private int room;
    public int CurrentRoomIndex => room;

    private Checkpoint currentCheckpoint;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
    }

    private void Die()
    {
        // Temp
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void OnCheckpointTouched(GameObject checkpointGo)
    {
        if (!checkpointGo.TryGetComponent(out Checkpoint checkpoint))
            return;

        if (currentCheckpoint == checkpoint)
            return;
        
        SetCurrentCheckpoint(checkpoint);
    }
    private void SetCurrentCheckpoint(Checkpoint checkpoint)
    {
        currentCheckpoint = checkpoint;
    }
}
