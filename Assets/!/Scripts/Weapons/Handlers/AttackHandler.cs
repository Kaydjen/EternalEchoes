using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackHandler : MonoBehaviour, IGameplayModeSwitcher, IUpdate
{
    #region VARIABLES
    [NonSerialized] public UnityEvent OnAttack = new();
    [SerializeField] private EAttackSide _type = EAttackSide.LMB;
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private bool _isAutomatic;
    private bool _isManualMode;
    private float _nextFireTime;
    private IAttack _attack;

    private Dictionary<EAttackSide, Action> _attackBindSubscribers;
    private Dictionary<EAttackSide, Action> _attackBindUnsubscribers;
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
    [ContextMenu("ChangeAttackButton")]
    public void ChangeAttackButton(EAttackSide type)
    {
        ForAIMode();
        _type = type;
        if(this.gameObject.activeSelf && _isManualMode) ForManualMode();
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
        _attackBindSubscribers = new()
        {
            { EAttackSide.LMB, SubscribeOnLBM },
            { EAttackSide.RMB, SubscribeOnRBM },
            { EAttackSide.F, SubscribeOnF }
        };
        _attackBindUnsubscribers = new()
        {
            { EAttackSide.LMB, UnsubscribeOnLBM },
            { EAttackSide.RMB, UnsubscribeOnRBM },
            { EAttackSide.F, UnsubscribebeOnF }
        };
    }
    public void ForManualMode()
    {
        _isManualMode = true;
       // if (_attackBindSubscribers.TryGetValue(_type, out Action act)) act?.Invoke();        
    }
    public void ForAIMode()
    {
        _isManualMode = false;
        if (_attackBindUnsubscribers.TryGetValue(_type, out Action act)) act?.Invoke();
    }
    #endregion
    #region PRIVATE
    private void SubscribeOnLBM()
    {
        InputManager.OnAttackLMB.AddListener(ExecuteAttack);

        if (!_isAutomatic) return;
        InputManager.OnAttackLMBPerformed.AddListener(ExecuteAttackPerformed);
        InputManager.OnAttackLMBReleased.AddListener(ExecuteAttackReleased);
    }
    private void SubscribeOnRBM()
    {
        InputManager.OnAttackRMB.AddListener(ExecuteAttack);

        if (!_isAutomatic) return;
        InputManager.OnAttackRMBPerformed.AddListener(ExecuteAttackPerformed);
        InputManager.OnAttackRMBReleased.AddListener(ExecuteAttackReleased);
    }
    private void SubscribeOnF()
    {
        InputManager.OnAttackF.AddListener(ExecuteAttack);

        if (!_isAutomatic) return;
        InputManager.OnAttackFPerformed.AddListener(ExecuteAttackPerformed);
        InputManager.OnAttackFReleased.AddListener(ExecuteAttackReleased);
    }
    private void UnsubscribeOnLBM()
    {
        InputManager.OnAttackLMB.RemoveListener(ExecuteAttack);

        if (!_isAutomatic) return;
        InputManager.OnAttackLMBPerformed.RemoveListener(ExecuteAttackPerformed);
        InputManager.OnAttackLMBReleased.RemoveListener(ExecuteAttackReleased);
    }
    private void UnsubscribeOnRBM()
    {
        InputManager.OnAttackRMB.RemoveListener(ExecuteAttack);

        if (!_isAutomatic) return;
        InputManager.OnAttackRMBPerformed.RemoveListener(ExecuteAttackPerformed);
        InputManager.OnAttackRMBReleased.RemoveListener(ExecuteAttackReleased);
    }
    private void UnsubscribebeOnF()
    {
        InputManager.OnAttackF.RemoveListener(ExecuteAttack);

        if (!_isAutomatic) return;
        InputManager.OnAttackFPerformed.RemoveListener(ExecuteAttackPerformed);
        InputManager.OnAttackFReleased.RemoveListener(ExecuteAttackReleased);
    }
    #endregion
}
