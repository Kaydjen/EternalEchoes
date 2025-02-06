using UnityEngine;
using UnityEngine.Events;

[Component("Entry point", "This script ")]
public class InitializationManager : MonoBehaviour
{
    [Space(20)]
    [Header("Settings")]
    [SerializeField] private bool _doDestroy = true;
    [SerializeField] private bool _doWaitBeforeExecute = false;
    [SerializeField] private float _timeToExecute = 0f;
    [Space(20)]
    [Header("Init methods")]
    [SerializeField] private UnityEvent _inits;
    [SerializeField] private UnityEvent _objectsToEnable;
    private void Awake()
    {
        if (_doWaitBeforeExecute) Invoke(nameof(Execute), _timeToExecute);
        else Execute();
    }
    private void Execute()
    {
        _inits?.Invoke();
        _objectsToEnable?.Invoke();

        if (_doDestroy)
        {
            Debug.Log($"{nameof(InitializationManager)} was destroyed");
            Destroy(this);
        }
    }
    #region Manu
















    [ContextMenu("Do Something")]
    private void DoSomething()
    {
        Debug.Log("And what were you expecting to see here?");
    }
    [ContextMenu("If tired")]
    private void IfTired()
    {
        Debug.Log("drink coffee and keep working");
    }
    #endregion
}




