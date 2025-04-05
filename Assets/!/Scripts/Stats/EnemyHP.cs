using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour, IDamageable
{
    [SerializeField] float HP = 100f;

    public void GetDamage(int value)
    {
        HP -= value;
        if(HP <= 0) Destroy(gameObject);
    }
}
