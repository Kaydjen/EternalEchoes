using System;
using UnityEngine;
using UnityEngine.Events;

public class AttackHandler : MonoBehaviour, IGameplayModeSwitcher, IUpdate
{
    #region VARIABLES
    public EAttackSide Type{ get => _type; set => _type = value;}
    [NonSerialized] public UnityEvent OnAttack = new();
    [SerializeField] private EAttackSide _type = EAttackSide.Left;
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private bool _isAutomatic;
    private float _nextFireTime;
    private IAttack _attack;
    #endregion
    #region PUBLIC METHODS
    public void ExecuteAttack()
    {
        if (Time.time < _nextFireTime) return;
        _attack.Attack();
        OnAttack?.Invoke();
        _nextFireTime = Time.time + _fireRate;
    }
    public void ExecuteAttackPerformed()
    {
        RegisterUpdate();    
    }
    public void ExecuteAttackReleased()
    {
        UnregisterUpdate();
    }
    #endregion    
    #region Update
    public void PerformInitialUpdate()
    {
        ExecuteAttack();
    }
    public void PerformPreUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformFinalUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformLateUpdate()
    {
        throw new System.NotImplementedException();
    }
    private void RegisterUpdate()
    {
        Updater.Instance.RegisterUpdate(this, Updater.UpdateType.InitialUpdate);
    }
    private void UnregisterUpdate()
    {
        Updater.Instance.UnregisterUpdate(this, Updater.UpdateType.InitialUpdate);
    }
    #endregion
    #region MONOBEHAVIOUR
    private void Awake()
    {
        if (!TryGetComponent(out _attack)) Debug.Log("Error");
    }
    public void ForManualMode()
    {
        if(_type == EAttackSide.Left)
        {
            InputHandler.OnAttackLMB.AddListener(ExecuteAttack);

            if (!_isAutomatic) return;
            InputHandler.OnAttackLMBPerformed.AddListener(ExecuteAttackPerformed);
            InputHandler.OnAttackLMBReleased.AddListener(ExecuteAttackReleased);
        }
        else
        {
            InputHandler.OnAttackRMB.AddListener(ExecuteAttack);

            if (!_isAutomatic) return;
            InputHandler.OnAttackRMBPerformed.AddListener(ExecuteAttackPerformed);
            InputHandler.OnAttackRMBReleased.AddListener(ExecuteAttackReleased);
        }
    }
    public void ForAIMode()
    {
        if (_type == EAttackSide.Left)
        {
            InputHandler.OnAttackLMB.RemoveListener(ExecuteAttack);

            if (!_isAutomatic) return;
            InputHandler.OnAttackLMBPerformed.RemoveListener(ExecuteAttackPerformed);
            InputHandler.OnAttackLMBReleased.RemoveListener(ExecuteAttackReleased);
        }
        else
        {
            InputHandler.OnAttackRMB.RemoveListener(ExecuteAttack);

            if (!_isAutomatic) return;
            InputHandler.OnAttackRMBPerformed.RemoveListener(ExecuteAttackPerformed);
            InputHandler.OnAttackRMBReleased.RemoveListener(ExecuteAttackReleased);
        }
    }
    #endregion
}


