using System;
using UnityEngine;

namespace CoopHead
{
    public class CameraController : MonoBehaviour
    {
        private Player player;
        private int currentRoomIndex;
        private Camera cam;

        private void Awake()
        {
            cam = GetComponent<Camera>();
        }

        private void Start()
        {
            player = Player.instance;
            player.onRoomChanged += LerpToRoom;

            currentRoomIndex = -1;
            LerpToRoom(0);
        }

        private void LerpToRoom(int roomIndex)
        {
            if (roomIndex == currentRoomIndex)
                return;
            currentRoomIndex = roomIndex;

            var roomsManager = RoomsManager.instance;
            if (!roomsManager)
            {
                Debug.Log("Rooms Manager is null");
                return;
            }
        
            var room = roomsManager.GetRoom(roomIndex);
            transform.position = new Vector3(room.x, room.y, transform.position.z);
            
            UpdateOrthographicSize(room);
        }

        private void UpdateOrthographicSize(Rect roomRect)
        {
            //Scale camera aspect ratio to zoom the entire level
            Resolution currentResolution = Screen.currentResolution;
            float heightByWidth = currentResolution.height / (float) currentResolution.width;
            if (roomRect.height >= roomRect.width) cam.orthographicSize = roomRect.height * 0.5f;
            else cam.orthographicSize = roomRect.width * heightByWidth * 0.5f;
        }
    }
}