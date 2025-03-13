using UnityEngine;

public class GameplayModeSwitcher : MonoBehaviour
{
    private IGameplayModeSwitcher[] _scripts;

    public void UpdateScripts()
    {
        _scripts = GetComponentsInChildren<IGameplayModeSwitcher>(true);
    } 
    public void DirectMode()
    {
        if (_scripts == null) UpdateScripts();
        if (_scripts == null) return; // Тут это не нужно, но на всякий случай оставлю

        foreach (IGameplayModeSwitcher el in _scripts)
        {
            el.ForDirectMode();
        }
        Debug.Log(_scripts.Length);        
    }
    public void AIMode()
    {
        if (_scripts == null) UpdateScripts();
        if (_scripts == null) return; // Тут это не нужно, но на всякий случай оставлю
        
        foreach (IGameplayModeSwitcher el in _scripts)
        {
            el.ForAIMode();
        }
        Debug.Log(_scripts.Length);        
    }
}

