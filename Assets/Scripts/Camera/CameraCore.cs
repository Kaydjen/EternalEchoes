using UnityEngine;
public class CameraCore : MonoBehaviour
{
    public static CameraCore Instance;
    public void ExclusivityСheck()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance == this) return;
            Instance.enabled = false;
            Instance = this;
        }
    }
}

// obsorver