using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private HashSet<HighlightActivator> _selectedObjects = new();
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HighlightActivator highlight))
        {
            highlight.Enable();
            _selectedObjects.Add(highlight);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out HighlightActivator highlight))
        {
            highlight.Disable();
            _selectedObjects?.Remove(highlight);
        }
    }
    private void OnEnable()
    {
        foreach (HighlightActivator el in _selectedObjects)
            el.Disable();
        _selectedObjects.Clear();
    }
}