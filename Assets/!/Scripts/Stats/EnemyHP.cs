using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour, IDamageable
{
    [SerializeField] int HP = 100;
    private GameObject _lastAttacker;
    public void GetDamage(int value, GameObject attacker)
    {
        HP -= value;
        if(HP <= 0) Destroy(gameObject);
        _lastAttacker = attacker;
    }
    public void SetHp(int value) => HP = Mathf.Clamp(value, 0, 100000);
    public int GetHP() => HP;
    public GameObject GetAttacker() => _lastAttacker;
}
