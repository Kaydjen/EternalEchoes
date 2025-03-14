using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ComponentInfo("Entry Point",
    "Точка входу для методів: використовується, " +
    "щоб вказати послідовність першого спрацьовування методів ініціалізації в класах. " +
    "\nЗамінник методу Awake/Start. " +
    "\nПерший перелік - для ініціалізації. " +
    "\nДругий список для увімкнення об'єктів. " +
    "\nЄ можливість затримки перед запуском ініціалізації. " +
    "\nМожна видалити клас після завершення роботи.")]

public class InitializationManager : MonoBehaviour
{
    [Space(20)]
    [Header("Settings")]
    [SerializeField] private bool _doExecute = true;
    [Space(5)]
    [SerializeField] private bool _doDestroy = true;
    [SerializeField] private bool _doWait = false;
    [SerializeField] private bool _doWaitBeforeEnableList = false;
    [SerializeField] private float _timeToExecuteAll = 0f;
    [SerializeField] private float _timeToExecuteEnableList = 0f;
    [Space(20)]
    [Header("Init methods")]
    [SerializeField] private UnityEvent _inits;
    [SerializeField] private UnityEvent _objectsToEnable;
    [Space(20)]
    [Header("Temporary")]
    [SerializeField] private float _timeToCharacterEnable = 1f;
    [SerializeField] private UnityEvent _characters;

    private void Awake()
    {
        if(_doExecute) StartCoroutine(Execute());
    }
    private IEnumerator Execute()
    {
        if (_doWait) yield return new WaitForSeconds(_timeToExecuteAll);
        _inits?.Invoke();

        yield return new WaitForSeconds(_timeToCharacterEnable);
        _characters?.Invoke();

        if (_doWaitBeforeEnableList) yield return new WaitForSeconds(_timeToExecuteEnableList);
        _objectsToEnable?.Invoke();

        if(_doDestroy)
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




