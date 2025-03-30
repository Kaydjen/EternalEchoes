using UnityEngine;

[RequireComponent(typeof(AttackHandler))]
public class ProjectileInstantiater : MonoBehaviour, IAttack
{
    private Pool<Transform> _pool;
    [SerializeField] private float _manaCost;
    private Stamina _stamina;

    private void Start()
    {
        _stamina = this.transform.root.GetComponent<Stamina>();
        _pool = FireballPool.Instance;
    }
    public void Attack()
    {
        if (!_stamina.SubtractStamina(_manaCost)) return;
        Transform bullet = _pool.Get();
        bullet.position = this.transform.position;
    }
}
