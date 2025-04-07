using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class InitEvents : MonoBehaviour
{
    [SerializeField] private float _autoCleanupDelay = 5f;
    public static UnityEvent OnUpdateReady = new UnityEvent();
    public static UnityEvent OnFirstCharacterReady = new UnityEvent();
    public static UnityEvent OnCameraSwitcherReady = new UnityEvent();
    public static UnityEvent OnAIManagerOfGroupsReady = new UnityEvent();
    private void Awake()
    {
        ReInitAll();
        if (_autoCleanupDelay > 0)
            StartCoroutine(WaitinigCoroutine());        
    }
    private IEnumerator WaitinigCoroutine()
    {
        yield return new WaitForSeconds(_autoCleanupDelay);
        Destroy(this);
    }
    private void OnDestroy()
    {
        ClearAll();
    }
    private void ReInitAll()
    {
        if (OnUpdateReady == null)
            OnUpdateReady = new UnityEvent();

        if (OnFirstCharacterReady == null)
            OnFirstCharacterReady = new UnityEvent();

        if (OnCameraSwitcherReady == null)
            OnCameraSwitcherReady = new UnityEvent();

        if (OnAIManagerOfGroupsReady == null)
            OnAIManagerOfGroupsReady = new UnityEvent();
    }
    private void ClearAll()
    {
        OnUpdateReady?.RemoveAllListeners();
        OnUpdateReady = null;

        OnFirstCharacterReady?.RemoveAllListeners();
        OnFirstCharacterReady = null;

        OnCameraSwitcherReady?.RemoveAllListeners();
        OnCameraSwitcherReady = null;

        OnAIManagerOfGroupsReady?.RemoveAllListeners();
        OnAIManagerOfGroupsReady = null;
    }
}