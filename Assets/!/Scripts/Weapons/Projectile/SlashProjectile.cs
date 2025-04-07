using System.Collections.Generic;
using UnityEngine;

public class SlashProjectile : MonoBehaviour, IGetAttacker
{
    [SerializeField] private int _damage;
    private GameObject _lastAttacker;

    public void SetAttacker(GameObject attacker) => _lastAttacker = attacker;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Тригер активований: {other.name}");
        if (other.CompareTag("Enemy"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.GetDamage(_damage, _lastAttacker);
            }
        }
    }

}
