using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[ComponentInfo("Перемикач Виду Камери", 
    "\nКерує перемиканням між різними режимами камери (ізометричним, FPV та RTS). " +
    "\nВикористовує Singleton. " +
    "\nЄ три івенти для кожного виду відповідно." +
    "\nРеагує на натискання клавіші C, " +
    "змінюючи активний режим камери та викликаючи відповіднi Events виду камер.")]
public class CameraSwitcher : MonoBehaviour // TODO: там надо сделать оверлей камер, что бы для юай и для игры было
{ 
    #region variables
    public static CameraSwitcher Instance;

    public static UnityEvent OnFPV_Enable = new UnityEvent();
    public static UnityEvent OnIsometricV_Enable = new UnityEvent();
    public static UnityEvent OnTopDownV_Enable = new UnityEvent();

    private byte _index = 0;

    public enum View
    {
        FPV,
        IsometricV,
        TopDownV,
    }
    public static View CurrentView = View.FPV;
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
        GameEvents.OnCharacterChange.AddListener(FirstEnable);
    }
    /// <summary>
    /// Этот метод создан для одиночного срабатывания за всю игру, в момент когда появляется первый персонаж и мы на него переключаемся
    /// </summary>
    private void FirstEnable()
    {
        SwitchToFPV();
        OnFPV_Enable.Invoke();
        _index++;
        GameEvents.OnCharacterChange.RemoveListener(FirstEnable);
    }
    /// <summary>
    /// Это для переключения между видами по порядку с помощью клавиши C
    /// </summary>
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
                CurrentView = View.FPV;
                break;
            case 1:
                SwitchToIsometricV();
                OnIsometricV_Enable.Invoke();
                CurrentView = View.IsometricV;
                break;
            case 2:
                SwitchToTopDownV();
                OnTopDownV_Enable.Invoke();
                CurrentView = View.TopDownV;
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
