using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShowerProjectile : MonoBehaviour, IGetAttacker
{
    [SerializeField] int _damage;
    [SerializeField] float _intervals;
    private GameObject _lastAttacker;

    public void SetAttacker(GameObject attacker) => _lastAttacker = attacker;

    private Dictionary<IDamageable, Coroutine> _enemyCoroutines = new Dictionary<IDamageable, Coroutine>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageable enemy = other.GetComponent<IDamageable>();
            if (enemy != null && !_enemyCoroutines.ContainsKey(enemy))
            {
                Coroutine coroutine = StartCoroutine(DamageEnemyOverTime(enemy));
                _enemyCoroutines.Add(enemy, coroutine);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageable enemy = other.GetComponent<IDamageable>();
            if (enemy != null && _enemyCoroutines.ContainsKey(enemy))
            {
                StopCoroutine(_enemyCoroutines[enemy]);
                _enemyCoroutines.Remove(enemy);
            }
        }
    }

    private IEnumerator DamageEnemyOverTime(IDamageable enemy)
    {
        while (true)
        {
            enemy.GetDamage(_damage, _lastAttacker);
            yield return new WaitForSeconds(_intervals);
        }
    }

    private void OnParticleSystemStopped()
    {
        foreach (var coroutine in _enemyCoroutines.Values)
        {
            StopCoroutine(coroutine);
        }

        _enemyCoroutines.Clear();

        MeteorShowerPool.Instance.Return(this.transform);
    }
}
