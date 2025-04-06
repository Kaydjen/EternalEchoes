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
        if (_attackBindSubscribers.TryGetValue(_type, out Action act)) act?.Invoke();        
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
        InputHandler.OnAttackLMB.AddListener(ExecuteAttack);

        if (!_isAutomatic) return;
        InputHandler.OnAttackLMBPerformed.AddListener(ExecuteAttackPerformed);
        InputHandler.OnAttackLMBReleased.AddListener(ExecuteAttackReleased);
    }
    private void SubscribeOnRBM()
    {
        InputHandler.OnAttackRMB.AddListener(ExecuteAttack);

        if (!_isAutomatic) return;
        InputHandler.OnAttackRMBPerformed.AddListener(ExecuteAttackPerformed);
        InputHandler.OnAttackRMBReleased.AddListener(ExecuteAttackReleased);
    }
    private void SubscribeOnF()
    {
        InputHandler.OnAttackF.AddListener(ExecuteAttack);

        if (!_isAutomatic) return;
        InputHandler.OnAttackFPerformed.AddListener(ExecuteAttackPerformed);
        InputHandler.OnAttackFReleased.AddListener(ExecuteAttackReleased);
    }
    private void UnsubscribeOnLBM()
    {
        InputHandler.OnAttackLMB.RemoveListener(ExecuteAttack);

        if (!_isAutomatic) return;
        InputHandler.OnAttackLMBPerformed.RemoveListener(ExecuteAttackPerformed);
        InputHandler.OnAttackLMBReleased.RemoveListener(ExecuteAttackReleased);
    }
    private void UnsubscribeOnRBM()
    {
        InputHandler.OnAttackRMB.RemoveListener(ExecuteAttack);

        if (!_isAutomatic) return;
        InputHandler.OnAttackRMBPerformed.RemoveListener(ExecuteAttackPerformed);
        InputHandler.OnAttackRMBReleased.RemoveListener(ExecuteAttackReleased);
    }
    private void UnsubscribebeOnF()
    {
        InputHandler.OnAttackF.RemoveListener(ExecuteAttack);

        if (!_isAutomatic) return;
        InputHandler.OnAttackFPerformed.RemoveListener(ExecuteAttackPerformed);
        InputHandler.OnAttackFReleased.RemoveListener(ExecuteAttackReleased);
    }
    #endregion
}
