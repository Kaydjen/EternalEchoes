using UnityEngine;
using UnityEngine.Events;

public class CameraSwitcher : MonoBehaviour
{
    #region variables
    public static CameraSwitcher Instance;

    public static UnityEvent OnFPV_Enable = new UnityEvent();
    public static UnityEvent OnIsometricV_Enable = new UnityEvent();
    public static UnityEvent OnTopDownV_Enable = new UnityEvent();

    private byte _index = 0;
    #endregion
    #region Methods
    public void Init()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance == this) return;
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);
        #endregion Singleton

        InputHandler.OnCPressed.AddListener(Switcher);
        Switcher();
    }
    private void Switcher()
    {
        // Change value below if added new map. Value represent count of current maps
        if (_index > 2) _index = 0;

        // And add here new case
        switch (_index)
        {
            case 0:
                SwitchToFPV();
                OnFPV_Enable.Invoke();
                break;
            case 1:
                SwitchToIsometricV();
                OnIsometricV_Enable.Invoke();
                break;
            case 2:
                SwitchToTopDownV();
                OnTopDownV_Enable.Invoke();
                break;
        }
        _index++;
    }
    #endregion
    #region Switch methods
    public void SwitchToFPV()
    {
        InputHandler.Instance.SetFPVState(true);
        GetComponent<FPV>().enabled = true;
    }
    public void SwitchToIsometricV()
    {
        InputHandler.Instance.SetIsometricState(true);
        GetComponent<IsometricV>().enabled = true;
    }
    public void SwitchToTopDownV()
    {
        InputHandler.Instance.SetTopDownState(true);
        GetComponent<TopDownV>().enabled = true;
    }
    #endregion
}
