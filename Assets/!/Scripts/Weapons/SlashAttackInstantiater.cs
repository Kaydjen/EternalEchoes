using UnityEngine;

[RequireComponent(typeof(AttackHandler))]
public class SlashAttackInstantiater : MonoBehaviour, IAttack
{
    [SerializeField] private ESlashAttack _type;
    [SerializeField] private GameObject _player;
    private Pool<Transform> _pool;

    private void Update()
    {
        switch (_type)
        {
            case ESlashAttack.Default:
                _pool = SlashDefaultPool.Instance;
                GetComponent<AttackHandler>().SetNewFireRate(1f);
                break;
            case ESlashAttack.Ice:
                _pool = SlashIcePool.Instance;
                GetComponent<AttackHandler>().SetNewFireRate(1f);
                break;
            case ESlashAttack.Electricity:
                _pool = SlashElectricityPool.Instance;
                GetComponent<AttackHandler>().SetNewFireRate(1f);
                break;
        }
    }
    public void Attack()
    {
        Transform slash = _pool.Get();
        slash.GetComponent<IGetAttacker>().SetAttacker(this.gameObject);
        slash.position = this.transform.position;
    }
}