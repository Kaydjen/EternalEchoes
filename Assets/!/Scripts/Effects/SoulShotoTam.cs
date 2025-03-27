using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulShotoTam : MonoBehaviour
{

    [SerializeField] private GameObject Sphere;
    private Rigidbody rb;
    private Vector3 _targetPos = Vector3.zero;
    private Vector3 _playerPos = Vector3.zero;

    [SerializeField] private float _moveSpeed;

    [SerializeField] private float _startHeight = 5f;
    private int _currentTime;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _targetPos = rb.position + new Vector3(0, _startHeight, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _currentTime++;
        if( _currentTime < 100)
        {
            Vector3 smoothMove = Vector3.Lerp(rb.position, _targetPos, _moveSpeed * Time.deltaTime);
            rb.position = smoothMove;
        }
        else
        {
            Vector3 smoothMove = Vector3.Lerp(rb.position, Sphere.transform.position, _moveSpeed * Time.deltaTime);
            rb.position = smoothMove;
        }
    }
}
