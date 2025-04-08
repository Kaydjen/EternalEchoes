using UnityEngine;
using UnityEngine.AI;

public class SlashIceProjectile : MonoBehaviour, IGetAttacker
{
    [SerializeField] private int _damage;
    [SerializeField] private int _freezeDuration;
    private GameObject _lastAttacker;


    public void SetAttacker(GameObject attacker) => _lastAttacker = attacker;

    private void OnEnable()
    {
        transform.SetParent(PlayerCore.Instance.Component.GetBoth());
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                NavMeshAgent agent = other.GetComponent<NavMeshAgent>();

                damageable.GetDamage(_damage, _lastAttacker);

                FreezeCharacter.Instance.Freeze(agent, _freezeDuration);
            }
        }
    }
    private void OnParticleSystemStopped()
    {
        SlashIcePool.Instance.transform.SetParent(SlashIcePool.Instance.transform);
        SlashIcePool.Instance.Return(this.transform);
    }
}
