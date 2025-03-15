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
    public void ForManualMode()
    {
        InputHandler.OnAttackLMB.AddListener(Attack);
    }
    public void ForAIMode()
    {
        InputHandler.OnAttackLMB.RemoveListener(Attack);
    }
}

