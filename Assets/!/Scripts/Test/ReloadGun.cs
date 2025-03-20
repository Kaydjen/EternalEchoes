using UnityEngine;

class ReloadGun : MonoBehaviour, IReload, IGameplayModeSwitcher 
{
    [SerializeField] private int _ammoCount = 10;
    [SerializeField] private int _maxbulletsCount = 10;
    private int _bulletsCount = 10;
    public int BulletsCount
    {
        get { return _bulletsCount; }
        set 
        {
            _bulletsCount = value;
            if(_bulletsCount == 0)
            {
                Reload();
            }
            // WORK: add show bullets logic and update logic (idea: the bullets can be shown near the gun, or if it's magic on the bracelet)
            UpdateBulletCountInfo();
        }
    }
    public void Reload()
    {
        if(_ammoCount == 0)
        {
            // WORK: Add logic when the ammo count is 0 (we should discuss it)
        }
        else
        {
            BulletsCount = _maxbulletsCount;
        }
    }
    public void SubtractOne()
    {
        BulletsCount--;
    }
    private void UpdateBulletCountInfo()
    {
        Debug.Log("Bullet count: " + BulletsCount);
    }
    public void ForManualMode()
    {
        GetComponent<AttackHandler>().OnAttack.AddListener(SubtractOne);
        InputHandler.OnReload.AddListener(Reload);
    }
    public void ForAIMode()
    {
        GetComponent<AttackHandler>().OnAttack.RemoveListener(SubtractOne);
        InputHandler.OnReload.RemoveListener(Reload);
    }
}