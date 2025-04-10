using UnityEngine;
using UnityEngine.AI;

public class SlashElectricityProjectile : MonoBehaviour, IGetAttacker
{
    [SerializeField] private int _damage;
    [SerializeField] private float _stunDuration;
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

                ElectrifyCharacter.Instance.Electrify(agent, _stunDuration);
            }
        }
    }
    private void OnParticleSystemStopped()
    {
        SlashElectricityPool.Instance.transform.SetParent(SlashElectricityPool.Instance.transform);
        SlashElectricityPool.Instance.Return(this.transform);
    }
}
