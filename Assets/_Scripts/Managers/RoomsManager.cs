using System;
using System.Collections.Generic;
using OutFoxeed.MonoBehaviourBase;
using UnityEngine;

namespace CoopHead
{
    public partial class RoomsManager : SingletonBase<RoomsManager>
    {
        public Room[] rooms;
        public Room GetRoom(int index) => rooms[index];

        public void InspectorSetup()
        {
            SetupRooms();
            SetupCheckpoints();
        }

        private void SetupRooms()
        {
            rooms = FindObjectsOfType<Room>(true);
            for (int i = 0; i < rooms.Length; i++)
            {
                for (int j = 0; j < rooms.Length - 1; j++)
                {
                    bool finished = true;
                    if (String.CompareOrdinal(rooms[j].name, rooms[j + 1].name) >= 0)
                    {
                        (rooms[j], rooms[j + 1]) = (rooms[j + 1], rooms[j]);
                        finished = false;
                    }

                    if (finished)
                        break;
                }
            }
        }

        public int GetRoomIndex(Room room)
        {
            for (int i = 0; i < rooms.Length; i++)
            {
                if (room == rooms[i])
                    return i;
            }
            return -1;
        }
    }
}