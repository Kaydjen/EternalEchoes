using System.Collections;
using UnityEngine;

class GunBulletTrail : MonoBehaviour
{
    [SerializeField] private float _fakeBulletSpeed = 500f;
    [SerializeField] private float _bulletDist = 50f;
    [SerializeField] private Transform _firePos;
    private WaitForSeconds _trailDelay;
    private Pool<TrailRenderer> _pool;
    public void SetNewBullet(Pool<TrailRenderer> pool) => _pool = pool;
    private IEnumerator PerformBullet(Vector3 endPos)
    {
        TrailRenderer bullet = _pool.Get();
        bullet.transform.position = _firePos.position;
        float distance = Vector3.Distance(_firePos.position, endPos);
        float remainingDistance = distance;
        while (remainingDistance > 0)
        {
            bullet.transform.position = Vector3.Lerp(_firePos.position, endPos, 1 - (remainingDistance / distance));
            remainingDistance -= _fakeBulletSpeed * Time.deltaTime;
            yield return null;
        }
        bullet.transform.position = endPos;
        yield return _trailDelay;
        _pool.Return(bullet);
    }
    private void PerformShootDef() => StartCoroutine(PerformBullet(_firePos.position + AimDirection.Direction.forward * _bulletDist));
    private void PerformShootWithTarget(RaycastHit hitInfo) => StartCoroutine(PerformBullet(hitInfo.point));
    private void Awake()
    {
        if (!TryGetComponent(out AttackHandler attack)) Debug.Log($"{nameof(attack)} is null in {nameof(GunBulletTrail)}");
        else attack.OnAttack.AddListener(PerformShootDef);

        if (!TryGetComponent(out RayPerformer ray)) Debug.Log($"{nameof(ray)} is null in {nameof(GunBulletTrail)}");
        else ray.OnHitSomething += _ => PerformShootWithTarget(_);

        if(BulletPool.Instance == null) Debug.Log($"{nameof(BulletPool.Instance)} is null in {nameof(GunBulletTrail)}");
        else _pool = BulletPool.Instance;
        if (_pool == null) Debug.Log($"{nameof(_pool)} is null in {nameof(GunBulletTrail)}");
        else _trailDelay = new WaitForSeconds(_pool.Get().time);
    }
}
/*
 
 TODO: создать какой-то менеджер басейнов, что бы делая в него запрос, он создавал новый бассейн по нужде
 
 */

/*
 
     private IEnumerator PerformBullet(RaycastHit hitInfo)
    {
        Debug.Log("BulletTrail");
        TrailRenderer bullet = _pool.Get();
        Vector3 startPosition = bullet.transform.position;
        float distance = Vector3.Distance(startPosition, hitInfo.point);
        float remainingDistance = distance;
        while (remainingDistance > 0)
        {
            bullet.transform.position = Vector3.Lerp(startPosition, hitInfo.point, 1 - (remainingDistance / distance));
            remainingDistance -= _fakeBulletSpeed * Time.deltaTime;
            yield return null;
        }
        bullet.transform.position = hitInfo.point;
        yield return _trailDelay;
        _pool.Return(bullet);
    }
 
 
 */