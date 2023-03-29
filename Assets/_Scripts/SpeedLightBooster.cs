using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpeedLightBooster : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Light2D _light2D;

    [Header("Speed")] [SerializeField] private float _speedMax;
    
    [Header("Light intensity")]
    [SerializeField] private float _lightIntensityMax;
    private float _lightIntensityMin;
    [SerializeField] private Ease _lightIntensityLerpEase;

    private void Awake()
    {
        _lightIntensityMin = _light2D.intensity;
    }

    void Update()
    {
        float t = Mathf.Clamp01(_rb.velocity.magnitude / _speedMax);
        _light2D.intensity = DOVirtual.EasedValue(_lightIntensityMin, _lightIntensityMax,
            t, _lightIntensityLerpEase);
    }
}
