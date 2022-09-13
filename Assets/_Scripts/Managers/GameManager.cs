using OutFoxeed.MonoBehaviourBase;
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

        protected override void OnSetPause()
        {
            CurrentGameState = IsPaused ? GameState.Paused : lastGameState;
        }
    }
}