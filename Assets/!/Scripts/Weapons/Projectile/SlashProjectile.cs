using System.Collections.Generic;
using UnityEngine;

public class SlashProjectile : MonoBehaviour, IGetAttacker
{
    [SerializeField] private int _damage;
    private GameObject _lastAttacker;

    public void SetAttacker(GameObject attacker) => _lastAttacker = attacker;

    private void OnEnable()
    {
        if(PlayerCore.Instance == null)
        {
            Debug.Log($"{nameof(PlayerCore.Instance)} is null is {nameof(this.name)}");
            InitEvents.OnFirstCharacterReady.AddListener(OnEnable);
            return;
        }
        transform.SetParent(PlayerCore.Instance.transform.GetChild(0));
        transform.localRotation = Quaternion.Euler(0,0,0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.GetDamage(_damage, _lastAttacker);
            }
        }
    }
    private void OnParticleSystemStopped()
    {
        SlashDefaultPool.Instance.transform.SetParent(SlashDefaultPool.Instance.transform);
        SlashDefaultPool.Instance.Return(this.transform);
    }
}
