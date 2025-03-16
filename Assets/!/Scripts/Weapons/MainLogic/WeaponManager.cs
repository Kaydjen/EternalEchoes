using UnityEngine;

public class WeaponManager : MonoBehaviour, IGameplayModeSwitcher 
{
    public AbstractWeapon Weapon;

    public void SetUp(AbstractWeapon weapon)
    {
        Weapon = weapon;
    }
    public void Attack()
    {
        Weapon.Attack();
    }
    public void EnterAimingMode()
    {
        Weapon.EnterAimingMode();
    }
    public void ExitAimingMode()
    {
        Weapon.ExitAimingMode();
    }
    public void ForManualMode()
    {
        InputHandler.OnAttackLMB.AddListener(Attack);
        InputHandler.OnEnterAimingMode.AddListener(EnterAimingMode);
        InputHandler.OnExitAimingMode.AddListener(ExitAimingMode);
    }
    public void ForAIMode()
    {
        InputHandler.OnAttackLMB.RemoveListener(Attack);
        InputHandler.OnEnterAimingMode.RemoveListener(EnterAimingMode);
        InputHandler.OnExitAimingMode.RemoveListener(ExitAimingMode);
    }
}

