﻿using OutFoxeed.MonoBehaviourBase;
using Rewired;
using UnityEngine;

namespace CoopHead
{
    public class GameManager : GameManagerBase<GameManager>
    {
        // Layer masks
        [SerializeField] private LayerMask groundLayerMask;
        public LayerMask GroundLayerMask => groundLayerMask;

        // Rewired
        private Rewired.Player rewiredPlayer;
        public Rewired.Player RewiredPlayer => rewiredPlayer;
        
        // Game state
        public enum GameState
        {
            Game,
            Paused,
            End,
        }
        private GameState currentGameState;
        public GameState CurrentGameState
        {
            get => currentGameState;
            set
            {
                if (currentGameState == value)
                    return;
                lastGameState = currentGameState;
                currentGameState = value;
            }
        }
        private GameState lastGameState;

        protected override void Awake()
        {
            base.Awake();
            rewiredPlayer = ReInput.players.GetSystemPlayer();
            
            CurrentGameState = GameState.Game;
        }

        private void Start()
        {
            rewiredPlayer ??= ReInput.players.GetSystemPlayer();
        }

        protected override void OnSetPause()
        {
            CurrentGameState = IsPaused ? GameState.Paused : lastGameState;
        }
    }
}