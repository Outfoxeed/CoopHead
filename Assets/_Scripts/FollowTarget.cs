using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Transform _target;
    [SerializeField] private float _followSpeedMax;
    [SerializeField] private Ease _followSpeedEase;

    [Header("Ranges")]
    [SerializeField] private float _targetRangeMin;
    [SerializeField] private float _targetRangeMax;

    private void Awake()
    {
        transform.position = _target.position;
    }

    private void LateUpdate()
    {
        Vector3 position = transform.position;
        Vector2 targetDir = _target.position - position;
        float targetDistance = targetDir.magnitude;
        targetDir.Normalize();

        if (targetDistance < _targetRangeMin)
        {
            return;
        }

        if (targetDistance > _targetRangeMax)
        {
            position = _target.position - (Vector3) (targetDir * _targetRangeMax);
        }

        float speed = DOVirtual.EasedValue(0f, _followSpeedMax, targetDistance / _targetRangeMax, _followSpeedEase);
        position += (Vector3)targetDir * (speed * Time.deltaTime);

        if (Mathf.Abs(position.x) >= float.PositiveInfinity || Mathf.Abs(position.y) >= float.PositiveInfinity)
        {
            return;
        }

        transform.position = position;
    }
}
