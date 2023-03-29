using System;
using UnityEngine;
using UnityEngine.Events;

namespace CoopHead
{
    public class CollisionDetection : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _minimumCollisionSpeed;
        [SerializeField] private UnityEvent _collisionResponse;
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (_rb.velocity.magnitude < _minimumCollisionSpeed)
            {
                return;
            }

            Vector2 contactPoint = col.GetContact(0).point;
            if (Mathf.Abs(_rb.position.x - contactPoint.x) < Mathf.Abs(_rb.position.y - contactPoint.y))
            {
                return;
            }
            
            _collisionResponse?.Invoke();
        }
    }
}