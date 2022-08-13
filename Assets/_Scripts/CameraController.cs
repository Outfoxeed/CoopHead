using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Player player;
    private int currentRoomIndex;

    private void Start()
    {
        player = Player.Instance;
    }

    private void Update()
    {
        if (currentRoomIndex != player.CurrentRoomIndex)
        {
            // LerpToRoom(player.CurrentRoomIndex);        
        }   
    }

    private void LateUpdate()
    {
                
    }

    //TODO: List of rooms, player knowing in which room he is. Camera TPing to player current rrom
    private void LerpToRoom()
    {
        
    }
}