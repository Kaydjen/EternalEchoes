using UnityEngine;

public class EnemyFlameAttack : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private int _damageMin;
    [SerializeField] private int _damageMax;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Transform _position;
    private Collider[] _hitColliders;
    public void Attack()
    {
        _hitColliders = Physics.OverlapSphere(_position.position, _radius, _layer);
        if(_hitColliders.Length == 0) return;
        Debug.Log("ZA ODRUU");
        foreach(Collider hitCollider in _hitColliders)
        {
            Debug.Log("ZA ALIANS!!!!!!!");
            if(!hitCollider.TryGetComponent(out IDamageable hp)) continue;
            else
            {
                hp.GetDamage(Random.Range(_damageMin, _damageMax), this.gameObject);
                Debug.Log("ZA PECZEŃKI!!!!!!!");
            }
        }        
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white; // Change color if needed
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
#endif
}
