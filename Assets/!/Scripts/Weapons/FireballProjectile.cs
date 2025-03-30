using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _speed;

    // Start is called before the first frame update
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>(); 
    }

    private void OnEnable()
    {
        if (AimDirection.Direction == null) return;
        _rb.AddForce(AimDirection.Direction.forward * _speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
      //  FireballPool.Instance.Return(this.transform);
    }

}
