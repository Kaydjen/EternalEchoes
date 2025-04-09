using UnityEngine;

public class EnemyHP : MonoBehaviour, IDamageable
{
    [Header("Settings")]
    [SerializeField] private int _hpMax = 100;
    [Space(5)]
    [Header("Components")]
    [SerializeField] private AI _ai;
    private GameObject _lastAttacker;
    private int _hp = 100;
    public void GetDamage(int value, GameObject attacker)
    {
        if(_hp == _hpMax) _ai.OnFirstHit?.Invoke(attacker);
        _hp -= value;
        if(_hp <= 0) Destroy(gameObject);
        _lastAttacker = attacker;
    }
    public void SetHp(int value) => _hp = Mathf.Clamp(value, 0, 100000);
    public int GetHP() => _hp;
    public GameObject GetAttacker() => _lastAttacker;
    private void OnEnable()
    {
        _hp = _hpMax;
    }
}
