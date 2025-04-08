using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnDestroyHandler : MonoBehaviour
{
    public UnityEvent onDestroyEvent = new();
    private void OnDestroy() => onDestroyEvent.Invoke();
}
