using UnityEngine;

class GunBulletTrail : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
   
    private void Awake()
    {
        RayPerformer rayPerformer = GetComponent<RayPerformer>();
     //  rayPerformer.OnHitDamageable += 

    }
}
