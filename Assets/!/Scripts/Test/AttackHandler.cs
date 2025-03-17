using UnityEngine;
using UnityEngine.Events;

public class AttackHandler : MonoBehaviour, IGameplayModeSwitcher
{
    #region VARIABLES
    public IAttack Attack;
    public UnityEvent OnAttack = new();
    #endregion
    #region PUBLIC METHODS
    public void ExecuteAttack()
    {
        Attack.Attack();
        OnAttack.Invoke();
    }
    #endregion
    #region MONOBEHAVIOUR
    private void Awake()
    {
        if (!TryGetComponent(out Attack)) Debug.Log("Error");
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
