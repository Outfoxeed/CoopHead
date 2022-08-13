using System.Collections.Generic;
using UnityEngine;

namespace CoopHead
{
    public class RoomsManager : MonoBehaviour
    {
        public Rect[] rooms;

        
        
        public struct Room
        {
            
        }

        public void UpdateRooms()
        {
            List<Rect> newRooms = new List<Rect>();

            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i).gameObject;
                if (child.TryGetComponent(out BoxCollider2D childCollider))
                {
                     
                }
            }
        }
    }
}