using UnityEngine;

public class EnemyHP : MonoBehaviour, IDamageable
{
    [Header("Settings")]
    [SerializeField] private int _hpMax = 100;
    private GameObject _lastAttacker;
    private int _hp = 100;
    private AI _ai;
    public void GetDamage(int value, GameObject attacker)
    {
        if(_hp == _hpMax) _ai.OnFirstHit?.Invoke(attacker);
        _hp -= value;
        if(_hp <= 0)
        {
            _ai.Unregister();            
        }
        _lastAttacker = attacker;
    }
    public void SetHp(int value) => _hp = Mathf.Clamp(value, 0, 100000);
    public int GetHP() => _hp;
    public GameObject GetAttacker() => _lastAttacker;
    private void OnEnable()
    {
        _hp = _hpMax;
    }
    private void Awake()
    {
        if(!TryGetComponent(out _ai))
        {
            Debug.Log($"{nameof(_ai)} is null in {this.name}");
            return;
        }
    }
}
