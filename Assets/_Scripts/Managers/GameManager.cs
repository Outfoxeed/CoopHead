using System;
using OutFoxeed.MonoBehaviourBase;
using Rewired;
using UnityEngine;
using UnityEngine.SceneManagement;

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

                if (currentGameState == GameState.End)
                    onGameEnd?.Invoke();
            }
        }
        private GameState lastGameState;
        public System.Action onGameEnd;

        protected override void Awake()
        {
            base.Awake();
            
            CurrentGameState = GameState.Game;
        }

        private void Start()
        {
            rewiredPlayer = ReInput.players.GetPlayer(0);
        }

        private void Update()
        {
            if (rewiredPlayer is null)
                return;

            if (rewiredPlayer.GetButtonDown("Pause"))
                TogglePause();
        }

        protected override void OnSetPaused()
        {
            CurrentGameState = IsPaused ? GameState.Paused : lastGameState;
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}