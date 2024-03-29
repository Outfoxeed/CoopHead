﻿using OutFoxeed.Attributes;
using UnityEngine;
#if UNITY_EDITOR
using System;
using UnityEngine.Tilemaps;
#endif

namespace CoopHead
{
    public class Room : MonoBehaviour
    {
        // Checkpoints
        [SerializeField] private Checkpoint[] checkpoints;
        public Checkpoint[] Checkpoints => checkpoints;

        // Collider
        [ReadOnly, SerializeField] private BoxCollider2D roomCollider;
        public bool IsRoomCollider(Collider2D other) => other == roomCollider;

        // Room rect
        private Rect rect;
        public Rect Rect => rect;
        
        void Awake()
        {
            rect = new Rect((Vector2) transform.position + roomCollider.offset, roomCollider.size);
        }

#if UNITY_EDITOR
        public void InspectorSetup()
        {
            SetupRoomCollider();
            SetupCheckpoints();
            SetupTileMaps();
        }

        private void SetupRoomCollider()
        {
            roomCollider = GetComponent<BoxCollider2D>();
            if (!roomCollider.isTrigger)
                roomCollider.isTrigger = true;
        }

        private void SetupTileMaps()
        {
            Tilemap[] tilemaps = GetComponentsInChildren<Tilemap>();
            for (int i = 0; i < tilemaps.Length; i++)
            {
                TryRenameRoomComponent<Tilemap>(tilemaps[i]);
            }
        }

        private void TryRenameRoomComponent<T>(T tilemap) where T : Component
        {
            string[] splits = tilemap.name.Split(" - ");
            string wantedName = $"{gameObject.name} - {splits[^1]}"; 
            if (tilemap.name == wantedName)
                return;
            tilemap.name = wantedName;
        }

        private void SetupCheckpoints()
        {
            checkpoints = gameObject.GetComponentsInChildren<Checkpoint>();
            for (int i = 0; i < checkpoints.Length; i++)
            {
                bool finished = true;
                for (int j = 0; j < checkpoints.Length - 1; j++)
                {
                    if (String.CompareOrdinal(checkpoints[i].name, checkpoints[i + 1].name) >= 0)
                    {
                        (checkpoints[i], checkpoints[i + 1]) = (checkpoints[i + 1], checkpoints[i]);
                        finished = false;
                    }
                }

                if (finished)
                    break;
            }

            // rename
            for (int i = 0; i < checkpoints.Length; i++)
            {
                TryRenameRoomComponent<Checkpoint>(checkpoints[i]);
            }
        }
#endif
    }
}
