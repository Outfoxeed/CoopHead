using System;
using CoopHead;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Player player;
    private int currentRoomIndex;

    private void Start()
    {
        player = Player.Instance;
        player.onRoomChanged += LerpToRoom;

        currentRoomIndex = -1;
        LerpToRoom(0);
    }

    private void LerpToRoom(int roomIndex)
    {
        if (roomIndex == currentRoomIndex)
            return;
        currentRoomIndex = roomIndex;

        var roomsManager = RoomsManager.Instance;
        if (!roomsManager)
        {
            Debug.Log("Rooms Manager is null");
            return;
        }
        
        var room = roomsManager.GetRoom(roomIndex);
        transform.position = new Vector3(room.x, room.y, transform.position.z);
    }
}