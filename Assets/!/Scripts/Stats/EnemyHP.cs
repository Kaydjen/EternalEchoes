using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour, IDamageable
{
    [SerializeField] int HP = 100;

    public void GetDamage(int value)
    {
        HP -= value;
        if(HP <= 0) Destroy(gameObject);
    }
    public void SetHp(int value) => HP = Mathf.Clamp(value, 0, 100000);
    public int GetHP() => HP;
}
