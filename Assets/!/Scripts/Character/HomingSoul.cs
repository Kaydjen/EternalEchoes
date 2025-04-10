using UnityEngine;

public class HomingSoul : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private Rigidbody rb;
    private Vector3 _targetPos = Vector3.zero;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _startHeight = 5f;
    private int _currentTime;
    private byte _soulValue = 1;

    public void SetSoulValue(byte value) => _soulValue = value;
    public void SetDestination(Transform target) => _target = target;
    void OnEnable()
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
            Vector3 smoothMove = Vector3.Lerp(rb.position, _target.position, _moveSpeed * Time.deltaTime);
            rb.position = smoothMove;

            // ¬иправленн€: перев≥р€Їмо в≥дстань до гравц€, а не до smoothMove
            if (Vector3.Distance(rb.position, _target.position) <= 0.5f)
            {
                DestroySoul();
            }
        }
    }

    private void DestroySoul()
    {
        if (_target.CompareTag("Enemy") && _target.TryGetComponent(out ISoulsLevelsHandler target))
            target.IncreaseSoulLevel();
        else if(_target.TryGetComponent(out SoulsBank characterSoul))
            characterSoul.Replenish(_soulValue);

        Destroy(gameObject);
    }

}
