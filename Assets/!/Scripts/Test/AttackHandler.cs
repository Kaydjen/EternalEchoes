using UnityEngine;

public class AttackHandler : MonoBehaviour, IGameplayModeSwitcher
{
    #region VARIABLES
    public IAttack Attack;
    #endregion
    #region PUBLIC METHODS
    public void ExecuteAttack()
    {
        Attack.Attack();
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
