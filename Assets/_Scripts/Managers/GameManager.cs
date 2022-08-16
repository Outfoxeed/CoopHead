using OutFoxeed.MonoBehaviourBase;
using Rewired;
using UnityEngine;

namespace CoopHead
{
    public class GameManager : GameManagerBase<GameManager>
    {
        [SerializeField] private LayerMask groundLayerMask;
        public LayerMask GroundLayerMask => groundLayerMask;

        private Rewired.Player rewiredPlayer;
        public Rewired.Player RewiredPlayer => rewiredPlayer;

        protected override void Awake()
        {
            base.Awake();
            rewiredPlayer = ReInput.players.GetSystemPlayer();
        }

        private void Start()
        {
            if(rewiredPlayer == null) rewiredPlayer = ReInput.players.GetSystemPlayer();
        }
    }
}