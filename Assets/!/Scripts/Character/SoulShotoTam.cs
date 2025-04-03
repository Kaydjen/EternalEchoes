using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulShotoTam : MonoBehaviour
{
    [SerializeField] private GameObject Sphere;
    private Rigidbody rb;
    private Vector3 _targetPos = Vector3.zero;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _startHeight = 5f;
    private int _currentTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _targetPos = rb.position + new Vector3(0, _startHeight, 0);
    }

    void Update()
    {
        _currentTime++;

        if (_currentTime < 100)
        {
            Vector3 smoothMove = Vector3.Lerp(rb.position, _targetPos, _moveSpeed * Time.deltaTime);
            rb.position = smoothMove;
        }
        else
        {
            Vector3 targetPos = Sphere.transform.position;
            Vector3 smoothMove = Vector3.Lerp(rb.position, targetPos, _moveSpeed * Time.deltaTime);
            rb.position = smoothMove;

            // ¬иправленн€: перев≥р€Їмо в≥дстань до гравц€, а не до smoothMove
            if (Vector3.Distance(rb.position, targetPos) <= 0.5f)
            {
                DestroySoul();
            }
        }
    }

    private void DestroySoul()
    {
        Destroy(gameObject);
    }

}
