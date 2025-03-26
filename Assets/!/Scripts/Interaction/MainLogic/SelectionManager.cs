using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HighlightActivator highlight)) highlight.Enable();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out HighlightActivator highlight)) highlight.Disable();
    }
}