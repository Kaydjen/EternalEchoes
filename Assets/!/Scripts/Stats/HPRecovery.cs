using UnityEngine;

public class HPRecovery : Stats, IGameplayModeSwitcher
{
    public override ESliderType Type { get => ESliderType.Recovery; protected set { } }
    private HP _hp;
    public void Recovery()
    {
        if (_hp == null) return;
        if (currentValue != 0)
        {
            _hp.GetRecovery(currentValue);
            currentValue = 0;
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
        currentValue += value;
    }
    public void ForAIMode()
    {
        InputHandler.OnHPRecovery.AddListener(Recovery);
    }
    public void ForManualMode()
    {
        InputHandler.OnHPRecovery.RemoveListener(Recovery);
    }
    private void Start()
    {
        if (!TryGetComponent(out _hp)) Debug.Log($"HPRecovery couldn't get HP component");
    }
}
