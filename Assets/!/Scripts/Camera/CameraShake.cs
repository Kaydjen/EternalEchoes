using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _magnitude;
    private Vector3 ogPos;
    private Quaternion ogRot;

    public static CameraShake Instance;

    private void Awake()
    {
        Instance = this;
        ogPos = transform.localPosition;
        ogRot = transform.localRotation;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Anus(_duration, _magnitude);
        }
    }

    public void Anus(float duration, float magnitude)
    {
        _duration = duration;
        _magnitude = magnitude;
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsed = 0.0f;

        while (elapsed < _duration) 
        {
            float xPos = Random.Range(-1f, 1f) * _magnitude;
            float yPos = Random.Range(-1f, 1f) * _magnitude;

            float xRot = Random.Range(-20f, 20f) * _magnitude;
            float yRot = Random.Range(-20f, 20f) * _magnitude;

            transform.localPosition = ogPos + new Vector3(xPos, yPos);

            transform.localRotation = transform.localRotation * Quaternion.Euler(xRot, yRot, 0f);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = ogPos;
        transform.localRotation = ogRot;
    }
}
