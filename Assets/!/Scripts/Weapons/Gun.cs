using UnityEngine;

public class Gun : AbstractWeapon, IGameplayModeSwitcher
{
    public override void Attack()
    {
        Debug.Log("BAM!!!");
    }
    public void ForDirectMode()
    {
        InputHandler.OnAttackLMB.AddListener(Attack);
    }
    public void ForAIMode()
    {
        InputHandler.OnAttackLMB.RemoveListener(Attack);
    }
}
