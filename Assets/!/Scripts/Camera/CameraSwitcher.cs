using UnityEngine;
using UnityEngine.Events;

[ComponentInfo("Перемикач Виду Камери",
    "\nКерує перемиканням між різними режимами камери (ізометричним, FPV та RTS). " +
    "\nВикористовує Singleton. " +
    "\nЄ три івенти для кожного виду відповідно." +
    "\nРеагує на натискання клавіші C, " +
    "змінюючи активний режим камери та викликаючи відповіднi Events виду камер.")]
public class CameraSwitcher : MonoBehaviour 
{
    #region variables
    public static CameraSwitcher Instance;

    public static UnityEvent OnFPV_Enable = new UnityEvent();
    public static UnityEvent OnIsometricV_Enable = new UnityEvent();
    public static UnityEvent OnTopDownV_Enable = new UnityEvent();

    private byte _index = 0;

    private ICamera _currentView;

    private FPV _FPV;
    private IsometricV _IsometricV;
    private TopDownV _TopDownV;
    #endregion
    #region Methods
    public void Awake()
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
        InitEvents.OnCameraSwitcherInit?.Invoke();
        DontDestroyOnLoad(gameObject);
        #endregion Singleton

        if (!TryGetComponent(out _FPV)) Debug.LogError($"{nameof(_FPV)} wasn't got in {nameof(CameraSwitcher)}");
        if (!TryGetComponent(out _IsometricV)) Debug.LogError($"{nameof(_IsometricV)} wasn't got in {nameof(CameraSwitcher)}");
        if (!TryGetComponent(out _TopDownV)) Debug.LogError($"{nameof(_TopDownV)} wasn't got in {nameof(CameraSwitcher)}");

        if (InputHandler.OnCPressed == null) Debug.LogError($"{nameof(InputHandler.OnCPressed)} wasn't got in {nameof(CameraSwitcher)} because it's not initialized");
        InputHandler.OnCPressed?.AddListener(Switcher);
        GameEvents.OnCharacterChange?.AddListener(FirstEnable);
    }
    /// <summary>
    /// Этот метод создан для одиночного срабатывания за всю игру, в момент когда появляется первый персонаж и мы на него переключаемся
    /// </summary>
    private void FirstEnable()
    {
        SwitchToFPV();
        OnFPV_Enable?.Invoke();
        _index++;
        GameEvents.OnCharacterChange?.RemoveListener(FirstEnable);
    }
    /// <summary>
    /// It swaps between Views in order by pressing C button 
    /// </summary>
    private void Switcher()
    {
        // Change value below if added new map. Value represent count of currentValue maps
        if (_index > 2) _index = 0;
        // And add here new case
        switch (_index)
        {
            case 0:
                SwitchToFPV();
                break;
            case 1:
                SwitchToIsometricV();
                break;
            case 2:
                SwitchToTopDownV();
                break;
        }
        _index++;
    }
    #endregion
    #region Switch methods
    public void SwitchToFPV()
    {
        InputHandler.Instance?.SetFPVState(true);
        _FPV.enabled = true;
        OnFPV_Enable?.Invoke();
        _currentView = _FPV;
    }
    public void SwitchToIsometricV()
    {
        InputHandler.Instance?.SetIsometricState(true);
        _IsometricV.enabled = true;
        OnIsometricV_Enable?.Invoke();
        _currentView = _IsometricV;
    }
    public void SwitchToTopDownV()
    {
        InputHandler.Instance?.SetTopDownState(true);
        _TopDownV.enabled = true;
        OnTopDownV_Enable?.Invoke();
        _currentView = _TopDownV;
    }
    /// <summary>
    /// Disable currentValue camera View and remove listener from switch button
    /// </summary>
    public void DisableCurrentView()
    {
        _currentView.Disable();
        InputHandler.OnCPressed?.RemoveListener(Switcher);
    }
    /// <summary>
    /// Enable currentValue camera View and add listener to switch button
    /// </summary>
    public void EnableCurrentView()
    {
        _currentView.Enable();
        InputHandler.OnCPressed?.AddListener(Switcher);
    }
    /// <summary>
    ///  It used when character was switched and we want to be sure currentValue player controls were switched correctly
    ///  | FPV -> Manual Controls (WASD) Character is controlled by Player
    ///  | IsometricV -> Manual Controls (WASD) Character is controlled by Player
    ///  | TopDown -> AI Controls (NavMesh) Character is controlled by AI logic
    /// </summary>
    public void UpdateControls()
    {
        if (_currentView == null) Debug.Log("Jest ze");
        _currentView.ForCharacterSwitch();
    }
    /// <summary>
    /// Unlock character ability to move or do something (Unlock Manual or AI controls)
    /// </summary>
    public void ManageControl()
    {
        _currentView.ForControlsManage();
    }
    /// <summary>
    /// Return which View type is currently activated
    /// </summary>
    /// <returns> FPV, IsometricV, TopDownV</returns>
    public ICamera GetViewType()
    {
        return _currentView;
    }
    /// <summary>
    /// Make clear is currentValue View activated or not
    /// </summary>
    /// <returns> True of false</returns>
    public bool IsCurrentViewEnabled()
    {
        return _currentView.IsEnabled();
    }
    #endregion

}