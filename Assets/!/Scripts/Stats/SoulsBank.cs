using UnityEngine;

public class SoulsBank : Stats, IGameplayModeSwitcher
{
    public override ESliderType Type { get => ESliderType.Recovery; protected set { } }
    private HP _hp;
    public void Recovery()
    {
        if (_hp == null) return;
        if (currentValue != 0)
        {
            _hp.GetRecovery(currentValue);
            Current = 0;
        }        
    }
    public int GetAllSouls()
    {
        int soulsCount = currentValue;
        Current = 0;
        return soulsCount;
    }
    public void Replenish(int value)
    {
        Current += value;
    }
    public void ForAIMode()
    {
        InputManager.OnHPRecovery.AddListener(Recovery);
    }
    public void ForManualMode()
    {
        InputManager.OnHPRecovery.RemoveListener(Recovery);
    }
    private void Start()
    {
        if (!TryGetComponent(out _hp)) Debug.Log($"SoulsBank couldn't get _hp component");
        ForAIMode();
    }
}
