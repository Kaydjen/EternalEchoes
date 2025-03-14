using UnityEngine;

public class GameplayModeSwitcher : MonoBehaviour
{
    private IGameplayModeSwitcher[] _scripts;

    public void UpdateScripts()
    {
        _scripts = GetComponentsInChildren<IGameplayModeSwitcher>(true);
    } 
    public void ManualMode()
    {
        if (_scripts == null) UpdateScripts();
        if (_scripts == null) return; // Тут это не нужно, но на всякий случай оставлю

        foreach (IGameplayModeSwitcher el in _scripts)
        {
            el.ForManualMode();
        }    
    }
    public void AIMode()
    {
        if (_scripts == null) UpdateScripts();
        if (_scripts == null) return; // Тут это не нужно, но на всякий случай оставлю
        
        foreach (IGameplayModeSwitcher el in _scripts)
        {
            el.ForAIMode();
        }     
    }
}

