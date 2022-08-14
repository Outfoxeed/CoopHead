using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace CoopHead
{
    public partial class RoomsManager : SingletonBase<RoomsManager>
    {
        public Rect[] rooms;
        public Rect GetRoom(int index) => rooms[index];

        public void UpdateRooms()
        {
            List<Rect> newRooms = new List<Rect>();
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i).gameObject;
                if (child.TryGetComponent(out BoxCollider2D childCollider))
                {
                    newRooms.Add(new Rect((Vector2) child.transform.position + childCollider.offset,
                        childCollider.size));
                    child.name = $"Room {newRooms.Count - 1}";
                }
                else
                {
                    Destroy(child);
                }
            }
            rooms = newRooms.ToArray();
        }
        public int GetRoomIndex(GameObject room)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject == room)
                    return i;
            }
            return -1;
        }
    }
}