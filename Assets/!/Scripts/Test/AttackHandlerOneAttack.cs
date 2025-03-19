using System;
using UnityEngine;
using UnityEngine.Events;

public class AttackHandlerOneAttack : MonoBehaviour, IGameplayModeSwitcher
{
    #region VARIABLES
    [NonSerialized] public UnityEvent OnAttack = new();
    private IAttack _attack;

    [SerializeField] private float _fireRate = 1f;
    private float _nextFireTime;
    #endregion
    #region PUBLIC METHODS
    public void ExecuteAttack()
    {
        if (Time.time < _nextFireTime) return;
        _attack.Attack();
        OnAttack.Invoke();
        _nextFireTime = Time.time + _fireRate;
    }
    #endregion
    #region MONOBEHAVIOUR
    private void Awake()
    {
        if (!TryGetComponent(out _attack)) Debug.Log("Error");
    }
    public void ForManualMode()
    {
        InputHandler.OnAttackLMB.AddListener(ExecuteAttack);
    }

    public void ForAIMode()
    {
        InputHandler.OnAttackLMB.RemoveListener(ExecuteAttack);
    }
    #endregion
}
