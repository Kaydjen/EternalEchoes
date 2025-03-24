using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private float _magnitude = 0.1f;
    public Vector3 ogPos;
    private Quaternion ogRot;
    private float _rotValue = 10f;

    public static CameraShake Instance;

    private void Awake()
    {
        Instance = this;
        CameraSwitcher.OnFPV_Enable.AddListener(() => _rotValue = 20f);
        CameraSwitcher.OnIsometricV_Enable.AddListener(() => _rotValue = 5f);
    }

    public void Anus(float duration, float magnitude)
    {
        ogPos = transform.localPosition;
        ogRot = transform.localRotation;
        _duration = duration;
        _magnitude = magnitude;
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsed = 0f;

        while (elapsed < _duration)
        {
            float damper = 1f - (elapsed / _duration); // Поступове згасання амплітуди
            float xPos = Mathf.PerlinNoise(Time.time * 10f, 0f) * 2f - 1f;
            float yPos = Mathf.PerlinNoise(0f, Time.time * 10f) * 2f - 1f;

            float xRot = Mathf.Sin(Time.time * 20f) * _rotValue * damper;
            float yRot = Mathf.Sin(Time.time * 15f) * _rotValue * damper;

            transform.localPosition = ogPos + new Vector3(xPos, yPos) * _magnitude * damper;
            transform.localRotation = transform.localRotation * Quaternion.Euler(xRot, yRot, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = ogPos;
        transform.localRotation = ogRot;
    }
}
