using UnityEngine;

class Gun : MonoBehaviour, IAttack
{
    [SerializeField] private bool _isShotgun;
    [SerializeField] private int _shotgunBulletCount = 2;
    [SerializeField] private float _critMultiplier = 2f;
    [SerializeField] private float _critChance = .05f;
    [SerializeField] private int _minDamage = 10;
    [SerializeField] private int _maxDamage = 20;
    [SerializeField] private bool _doSpread;
    [Tooltip("Remember: value .1f is for shotgun, so for just a gun make it about .01f or lover")]
    [SerializeField] private float _spreadRandomX = .01f; 
    [SerializeField] private float _spreadRandomY = .01f;
    [SerializeField] private int _rayDist = 15;
    [SerializeField] private LayerMask _layers;
    private IDamageable _damageable;
    private Vector3 _spread;
    private int _damage;
    private void CastRay()
    {
        if (_doSpread) _spread = new Vector3(Random.Range(-_spreadRandomX, _spreadRandomX), Random.Range(-_spreadRandomY, _spreadRandomY), 0f);
        else _spread = Vector3.zero;

        if (Physics.Raycast(AimDirection.Direction.position, (AimDirection.Direction.forward + _spread).normalized, out RaycastHit hitInfo, _rayDist, _layers))
        {
            if (hitInfo.transform.TryGetComponent(out _damageable))
            {
                if (Random.value < _critChance) 
                    _damage = (int)(Random.Range(_minDamage, _maxDamage) * _critMultiplier);
                else
                    _damage = Random.Range(_minDamage, _maxDamage);
                _damageable.GetDamage(_damage); 

                Debug.Log(_damage);
            }
            #if UNITY_EDITOR
            Debug.DrawRay(AimDirection.Direction.position, (AimDirection.Direction.forward + _spread).normalized * _rayDist, Color.cyan, 1f);
            #endif
        }
    }
    public void Attack()
    {
        if (_isShotgun)
        {
            for (int i = 0; i < _shotgunBulletCount; i++)
            {
                CastRay();
            }
        }
        else
        {
            CastRay();
            CameraShake.Instance.Anus(0.5f, 0.1f);
        }
    }
}
