using UnityEngine;

public class HighlightActivator : MonoBehaviour
{
    [SerializeField] private GameObject _highlightObj;
    public void Enable() => _highlightObj.SetActive(true);
    public void Disable() => _highlightObj.SetActive(false);
}
