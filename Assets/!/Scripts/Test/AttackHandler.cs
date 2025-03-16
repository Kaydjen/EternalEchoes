using UnityEngine;

/*
 * s - script
 * 
 */
public class AttackHandler : MonoBehaviour, IGameplayModeSwitcher
{
    #region VARIABLES
    public IAttack s_attack;
    #endregion
    #region PUBLIC METHODS
    public void SetAttack(IAttack attack)
    {
        s_attack = attack;
    }
    public void ExecuteAttack()
    {
        s_attack.Attack();
    }
    #endregion
    #region MONOBEHAVIOUR
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
