using System;
using OutFoxeed.UsefulStructs;
using UnityEngine;

namespace CoopHead
{
    [RequireComponent(typeof(Rigidbody2D))]
    public partial class PlayerController : MonoBehaviour
    {
        private Rewired.Player rewiredPlayer;
        private Rigidbody2D rb;
        private float startGravityScale;
        private bool blocked;
        [SerializeField] private bool debug;

        [Header("Movements")] [SerializeField] private float moveSpeed;
        private float horizontalInput;

        [Header("Jump")] [SerializeField] private float jumpNormalForce;
        [SerializeField] private float jumpBoostForce;
        [SerializeField] private Remember jumpPressedRemember;
        [SerializeField] private Remember groundedRemember;
        [SerializeField] private float maxFallSpeed = 25f;
        
        [Header("Super Boost")]
        [SerializeField] private float superBoostForce;
        [SerializeField, Range(0f, 10f)] private float superBoostAirControl;
        private bool superBoost;

        [Header("Ground Check")] [SerializeField]
        private Transform groundCheck;

        [SerializeField, Range(0, 5)] private float groundCheckDistance;

        private LayerMask groundLayerMask;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            startGravityScale = rb.gravityScale;
        }

        void Start()
        {
            rewiredPlayer = GameManager.instance.RewiredPlayer;

            groundLayerMask = GameManager.instance.GroundLayerMask;

            ResetCloseBoosts();
        }

        // Get inputs in the update loop
        void Update()
        {
            if (rewiredPlayer == null)
                return;

            // Remember when player pressing jump button
            if (rewiredPlayer.GetButtonDown("Jump"))
                jumpPressedRemember.Trigger();
            jumpPressedRemember.DecreaseRemember(Time.deltaTime);

            // Remember when player is touching the ground
            if (IsGrounded()) 
                groundedRemember.Trigger();
            groundedRemember.DecreaseRemember(Time.deltaTime);

            horizontalInput = rewiredPlayer.GetAxisRaw("HorizontalMove");
        }

        // Update rb to inputs in the fixed update loop
        private void FixedUpdate()
        {
            if (blocked)
                return;

            TryJump();

            // Horizontal Move
            // If not in super boost, overwrite the horizontal direction
            if(!superBoost)
                rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
            // Else, we give a slight air control to the player
            else
                rb.velocity += new Vector2(horizontalInput * moveSpeed * superBoostAirControl * Time.fixedDeltaTime, 0);
            
            // Limit fall speed
            if (rb.velocity.y <= -maxFallSpeed)
                rb.velocity = new Vector2(rb.velocity.x, -maxFallSpeed);
        }

        private void OnDrawGizmosSelected()
        {
            if (!groundCheck)
                return;

            Gizmos.color = Color.green;
            var groundCheckPos = groundCheck.transform.position;
            Gizmos.DrawLine(groundCheckPos, groundCheckPos + Vector3.down * groundCheckDistance);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            StopSuperBoost();
        }

        private void TryJump()
        {
            if (!jumpPressedRemember.IsRemembering())
                return;

            if(debug) Debug.Log("Jump");
            
            // Normal jump
            if (groundedRemember.IsRemembering())
            {
                JumpNormal();
                return;
            }

            // Boost jump
            if (HasCloseBoosts())
            {
                Transform boost = GetClosestBoost();
                if (!boost)
                    return;

                JumpBoost(superBoost);
                RemoveCloseBoost(boost);
                Destroy(boost.gameObject);
                return;
            }
        }

        public void StartSuperBoost(Vector2 dir)
        {
            rb.velocity = dir.normalized * superBoostForce;
            superBoost = true;
        }
        private void StopSuperBoost() => superBoost = false;

        public void BlockMovement(bool block)
        {
            rb.gravityScale = block ? 0 : startGravityScale;
            if(block) rb.velocity = Vector2.zero;
            blocked = block;
        }

        private void JumpNormal() => JumpBase(jumpNormalForce);
        private void JumpBoost(bool resetXVelocity) => JumpBase(jumpBoostForce, resetXVelocity);

        private void JumpBase(float jumpForce, bool resetXVelocity = false) // Not meant to be used
        {
            // In case
            StopSuperBoost();
            
            rb.velocity = new Vector2(resetXVelocity ? Mathf.Sign(rb.velocity.x) * moveSpeed : rb.velocity.x, jumpForce);

            jumpPressedRemember.Reset();
            groundedRemember.Reset();
        }

        bool IsGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(groundCheck.transform.position, Vector2.down,
                groundCheckDistance, groundLayerMask);
            return hit;
        }

        public void OnDeath()
        {
            BlockMovement(false);
            jumpPressedRemember.Reset();
            groundedRemember.Reset();
            
            // Boosts
            ResetCloseBoosts();
            boostsClose = new Transform[5];
        }
    }
}