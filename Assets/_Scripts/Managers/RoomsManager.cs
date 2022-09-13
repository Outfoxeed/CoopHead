using System;
using System.Collections.Generic;
using System.Linq;
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
            var temp = FindObjectsOfType<Room>(true).ToList();
            temp.Sort((x, y) => string.Compare(x.name, y.name));
            rooms = temp.ToArray();
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