using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    public static PlayerCore Instance;
    private void OnEnable()
    {
        if(Instance == null)
        {
            Instance = this;
            ChangeSwDMState(true);
        }
        else
        {
            if (Instance == this) return;

            Instance.ChangeSwDMState(false);
            Instance.enabled = false;

            Instance = this;
            ChangeSwDMState(true);
        }
        GameEvents.OnCharacterChange?.Invoke();
    }
    public void ChangeSwDMState(bool state) // SwapDrivenManager
    {
        transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(state);
    }
}
