using UnityEngine;

public class GameplayModeSwitcher : MonoBehaviour
{
    private IGameplayModeSwitcher[] _scripts;
    private void Awake()
    {
        _scripts = GetComponentsInChildren<IGameplayModeSwitcher>(true);
    }
    public void DirectMode()
    {
        if (_scripts == null) _scripts = GetComponentsInChildren<IGameplayModeSwitcher>(true);
        if (_scripts != null)
        {
            Debug.Log(_scripts.Length);
            foreach (var script in _scripts)
            {
                script.ForDirectMode();
            }
        }
    }
    public void AIMode()
    {
        if (_scripts == null) _scripts = GetComponentsInChildren<IGameplayModeSwitcher>(true);
        if (_scripts != null)
        {
            Debug.Log(_scripts.Length);
            foreach (var script in _scripts)
            {
                script.ForAIMode();
            }
        }
    }
}

