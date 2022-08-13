using System;
using Rewired;
using UnityEngine;

namespace CoopHead
{
    public class GameManager : SingletonBase<GameManager>
    {
        [SerializeField] private LayerMask groundLayerMask;
        public LayerMask GroundLayerMask => groundLayerMask;

        private Rewired.Player rewiredPlayer;
        public Rewired.Player RewiredPlayer => rewiredPlayer;

        private Camera mainCam;
        public Camera MainCam => mainCam;

        protected override void Awake()
        {
            base.Awake();
            rewiredPlayer = ReInput.players.GetSystemPlayer();
            mainCam = Camera.main;
        }

        private void Start()
        {
            if(rewiredPlayer == null) rewiredPlayer = ReInput.players.GetSystemPlayer();
        }
    }
}