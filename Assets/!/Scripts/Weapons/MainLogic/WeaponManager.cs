using UnityEngine;

public class WeaponManager : MonoBehaviour, IGameplayModeSwitcher 
{
    public AbstractWeapon<WeaponScriptableAbstract> AbstractWeapon;

    private void Start()
    {
        
    }
    public void SetUp(AbstractWeapon<WeaponScriptableAbstract> weapon)
    {
        AbstractWeapon = weapon;
    }
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

