using UnityEngine;

public class WeaponManager : MonoBehaviour, IGameplayModeSwitcher
{
    public WeaponScriptableAbstract Data;
    public AbstractWeapon AbstractWeapon;

    public void Attack()
    {
        AbstractWeapon.Attack();
    }
    public void ForManualMode()
    {
        InputHandler.OnAttackLMB.AddListener(Attack);
    }
    public void ForAIMode()
    {
        InputHandler.OnAttackLMB.RemoveListener(Attack);
    }
}

