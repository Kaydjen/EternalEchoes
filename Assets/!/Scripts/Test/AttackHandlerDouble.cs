using System;
using UnityEngine;
using UnityEngine.Events;

public class AttackHandlerDouble : MonoBehaviour, IGameplayModeSwitcher
{
    #region VARIABLES
    [NonSerialized] public UnityEvent OnAttackPerformed = new();
    [NonSerialized] public UnityEvent OnAttackReleased = new();
    private IAttackDouble _attack;
    #endregion
    #region PUBLIC METHODS
    public void ExecuteAttackPerformed()
    {
        _attack.AttackPerformed();
        OnAttackPerformed.Invoke();
    }
    public void ExecuteAttackReleased()
    {
        _attack.AttackReleased();
        OnAttackReleased.Invoke();
    }
    #endregion
    #region MONOBEHAVIOUR
    private void Awake()
    {
        if (!TryGetComponent(out _attack)) Debug.Log("Error");
    }
    public void ForManualMode()
    {
        InputHandler.OnAttackLMBPerformed.AddListener(ExecuteAttackPerformed);
        InputHandler.OnAttackLMBReleased.AddListener(ExecuteAttackReleased);
    }
    public void ForAIMode()
    {
        InputHandler.OnAttackLMBPerformed.RemoveListener(ExecuteAttackPerformed);
        InputHandler.OnAttackLMBReleased.RemoveListener(ExecuteAttackReleased);
    }
    #endregion
}