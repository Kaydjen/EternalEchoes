using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _speed;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _rb.AddForce(transform.forward * _speed, ForceMode.Impulse);
    }

}
