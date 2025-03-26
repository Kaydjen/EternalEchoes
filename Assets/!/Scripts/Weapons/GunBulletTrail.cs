using System.Collections;
using UnityEngine;

class GunBulletTrail : MonoBehaviour
{
    [SerializeField] private TrailRenderer _bulletTrailPrefab;
    [SerializeField] private Transform _gunBarrel;
    [SerializeField] private float _fakeBulletSpeed = 500f;
    private WaitForSeconds _trailDelay;
    private Pool<TrailRenderer> _pool;
/*    public void SetNewBullet(GameObject )
    {

    }*/
    private IEnumerator PerformBullet(RaycastHit hitInfo)
    {
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
    private void Awake()
    {
        RayPerformer rayPerformer = GetComponent<RayPerformer>();
        rayPerformer.OnHitDamageable += _ => StartCoroutine(PerformBullet(_));
        _trailDelay = new WaitForSeconds(_bulletTrailPrefab.time);
    }
}
