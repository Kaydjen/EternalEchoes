using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _magnitude;
    private Vector3 ogPos;

    public static CameraShake Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Anus(float duration, float magnitude)
    {
        ogPos = transform.localPosition;
        _duration = duration;
        _magnitude = magnitude;
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsed = 0.0f;

        while (elapsed < _duration) 
        {
            float yPos = Random.Range(-1f, 1f) * _magnitude;
            float xPos = Random.Range(-1f, 1f) * _magnitude;

            transform.localPosition = ogPos + new Vector3(xPos, yPos);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = ogPos;
    }
}
