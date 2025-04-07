using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class InitEvents : MonoBehaviour
{
    [SerializeField] private float _autoCleanupDelay = 5f;
    public static UnityEvent OnUpdateInit = new UnityEvent();
    public static UnityEvent OnFirstCharacterInit = new UnityEvent();
    public static UnityEvent OnCameraSwitcherInit = new UnityEvent();
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
        if (OnUpdateInit == null)
            OnUpdateInit = new UnityEvent();

        if (OnFirstCharacterInit == null)
            OnFirstCharacterInit = new UnityEvent();

        if (OnCameraSwitcherInit == null)
            OnCameraSwitcherInit = new UnityEvent();
    }
    private void ClearAll()
    {
        OnUpdateInit?.RemoveAllListeners();
        OnUpdateInit = null;

        OnFirstCharacterInit?.RemoveAllListeners();
        OnFirstCharacterInit = null;

        OnCameraSwitcherInit?.RemoveAllListeners();
        OnCameraSwitcherInit = null;
    }
}