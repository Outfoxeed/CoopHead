using Rewired;
using UnityEngine;

namespace CoopHead
{
    [RequireComponent(typeof(Rigidbody2D))]
    public partial class PlayerController : MonoBehaviour
    {
        private Rewired.Player rewiredPlayer;
        private Rigidbody2D rb;

        [Header("Movements")]
        [SerializeField] private float moveSpeed;
        private float horizontalInput;
        
        [Header("Jump")]
        [SerializeField] private float jumpNormalForce;
        [SerializeField] private float jumpBoostForce;
        [SerializeField] private Remember jumpPressedRemember;
        [SerializeField] private Remember groundedRemember;
        
        [Header("Ground Check")]
        [SerializeField] private Transform groundCheck;
        [SerializeField, Range(0,5)] private float groundCheckDistance;

        private LayerMask groundLayerMask;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            rewiredPlayer = GameManager.Instance.RewiredPlayer;

            groundLayerMask = GameManager.Instance.GroundLayerMask;

            boostsClose = new Transform[5];
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
            TryJump();

            // Horizontal Move
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        }

        private void OnDrawGizmosSelected()
        {
            if (!groundCheck)
                return;

            Gizmos.color = Color.green;
            var groundCheckPos = groundCheck.transform.position;
            Gizmos.DrawLine(groundCheckPos, groundCheckPos + Vector3.down * groundCheckDistance);
        }

        private void TryJump()
        {
            if (!jumpPressedRemember.IsRemembering())
                return;
            
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
                
                JumpBoost();
                RemoveCloseBoost(boost);
                Destroy(boost.gameObject);
                return;
            }
        }

        private void JumpNormal() => JumpBase(jumpNormalForce);
        private void JumpBoost() => JumpBase(jumpBoostForce);
        private void JumpBase(float jumpForce) // Not meant to be used
        {
            // Debug.Log("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            
            jumpPressedRemember.Reset();
            groundedRemember.Reset();
        }

        bool IsGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(groundCheck.transform.position, Vector2.down,
                groundCheckDistance, groundLayerMask);
            return hit;
        }
    }
}
