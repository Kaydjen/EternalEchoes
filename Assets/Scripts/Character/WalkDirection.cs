using UnityEngine;

public class WalkDirection : MonoBehaviour
{
    public static WalkDirection Instance;
    public void Init()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance == this) return;
            Debug.LogError($"{Instance.name} already exist");
            Instance = this;
        }
    }
    private void SetRotation()
    {
        this.transform.rotation = Quaternion.identity;
    }
    private void OnEnable()
    {
        CameraSwitcher.OnIsometricV_Enable.AddListener(SetRotation);
    }
    private void OnDisable()
    {
        CameraSwitcher.OnIsometricV_Enable.RemoveListener(SetRotation);
    }
}   
