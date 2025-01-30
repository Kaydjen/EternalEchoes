using UnityEngine;

public class AIPlSwapper : MonoBehaviour
{
    private void Start()
    {
        CameraSwitcher.OnFPV_Enable?.AddListener(ActivatePlayerControl);
        CameraSwitcher.OnIsometricV_Enable?.AddListener(ActivatePlayerControl);
        CameraSwitcher.OnTopDownV_Enable?.AddListener(ActivateAIControl);
    }
    private void ActivatePlayerControl()
    {
        this.transform.GetChild(1).gameObject.SetActive(true);
        this.transform.GetChild(2).gameObject.SetActive(false);
    }
    private void ActivateAIControl()
    {
        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(2).gameObject.SetActive(true);
    }
}
