using System;
using DG.Tweening;
using UnityEngine;

namespace CoopHead
{
    public class PlayerGfx : MonoBehaviour
    {
        private Vector3 _startScale;
        private Quaternion _startRotation;
            
        private void Awake()
        {
            _startScale = transform.localScale;
            _startRotation = transform.rotation;
        }

        public void ResetTransform()
        {
            transform.localScale = _startScale;
            transform.rotation = _startRotation;
            DOTween.Kill(gameObject, false);
        }
    }
}