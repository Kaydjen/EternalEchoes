using UnityEngine;

public class OBJOptionsManu : MonoBehaviour, IMenu
{
    [SerializeField] protected GameObject _manu;
    protected IInteractStrategy _context;
    #region PUBLIC
    public virtual void EnableMenu()
    {
        SetContext();
        SetTabs();
        SetMenuPanelState(true);
    }
    public virtual void DisableMenu()
    {
        SetMenuPanelState(false);
    }
    #region actions
    public void ExecuteAction1() 
    {
        _context.Action1();
        DisableMenu();
    }
    public void ExecuteAction2()
    {
        _context.Action2();
        DisableMenu();
    }
    public void ExecuteAction3()
    {
        _context.Action3();
        DisableMenu();
    }
    public void ExecuteAction4()
    {
        _context.Action4();
        DisableMenu();
    }
    public void ExecuteAction5()
    {
        _context.Action5();
        DisableMenu();
    }
    #endregion
    #endregion
    #region PROTECTED
    protected virtual void SetContext()
    {
        if (Hover.HitedCollider.TryGetComponent(out IInteractStrategy strategy))
            _context = strategy;
    }
    protected virtual void CreateTabs()
    {
        GetComponent<Tabs>().InitButtonMethods(new System.Action[] { ExecuteAction1, ExecuteAction2, ExecuteAction3, ExecuteAction4, ExecuteAction5 });
    }
    protected virtual void SetTabs()
    {
        GetComponent<Tabs>().CreateTabs(_context.GetData());
    }
    protected virtual void SetMenuPanelState(bool state)
    {
        _manu.SetActive(state);
    }
    #endregion
    #region MONOBEHAVIOUR
    protected virtual void Awake()
    {
        CreateTabs();
    }
    #endregion
}










/*
 
 
 using UnityEngine;

public class InteractOptions : MonoBehaviour, IMenu
{
    #region VARIABLES
    public static InteractOptions Instance { get; private set; }
    [SerializeField] private GameObject _manu;
    private IInteractStrategy _context;
    #endregion
    #region PUBLIC METHODS
    public void EnableMenu()
    {
        if(Hover.HitedCollider.TryGetComponent(out IInteractStrategy strategy))
            _context = strategy;
        GetComponent<Tabs>().CreateTabs(_context.GetData());
        InputManager.Instance.ActivateOptionsNumbersMap();
        _manu.SetActive(true);
        DisableComponents();
    }
    public void DisableMenu()
    {
        InputManager.Instance.ActivateDefNumbersMap();
        _manu.SetActive(false);
        ActivateComponents();
    }
    #region actions
    public void Action1() // TODO: тут мб класс надо будет переделать (вызовы ExecuteAction1)
    {
        _context.Action1();
        DisableMenu();
    }
    public void Action2()
    {
        _context.Action2();
        DisableMenu();
    }
    public void Action3()
    {
        _context.Action3();
        DisableMenu();
    }
    public void Action4()
    {
        _context.Action4();
        DisableMenu();
    }
    public void Action5()
    {
        _context.Action5();
        DisableMenu();
    }
    #endregion
    #endregion
    #region PRIVATE METHODS
    private void DisableComponents()
    {
        InputManager.Instance.SetAllAim(false);
        InputManager.Instance.SetAllAttack(false);
        InputManager.Instance.SetAllMovement(false);
        InputManager.Instance.SetAllInteract(false);
        InputManager.Instance.SetUIState(true); 

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        CameraSwitcher.Instance.DisableCurrentView();
        AIPlSwapper.LockControl();
    }
    private void ActivateComponents()
    {
        InputManager.Instance.SetAllAim(true);
        InputManager.Instance.SetAllAttack(true);
        InputManager.Instance.SetAllMovement(true);
        InputManager.Instance.SetAllInteract(true);
        InputManager.Instance.SetUIState(false);

        if (!CameraSwitcher.Instance.IsCurrentViewEnabled()) CameraSwitcher.Instance.EnableCurrentView();
        AIPlSwapper.UnlockControl();
    }
    #endregion
    #region MONO METHODS
    public void Init()
    {
        Instance = this;

        InputManager.OnOptionsOne.AddListener(Action1);
        InputManager.OnOptionsTwo.AddListener(Action2);
        InputManager.OnOptionsThree.AddListener(Action3);
        InputManager.OnOptionsFour.AddListener(Action4);
        InputManager.OnOptionsFive.AddListener(Action5);

        GetComponent<Tabs>().InitButtonMethods(new System.Action[] { Action1, Action2, Action3, Action4, Action5 });
    }
    private void OnEnable()
    {
        DynamicMenuManagerContext.Instance.RegisterMenu(EMenu.FPVMenu, this);
    }
    private void OnDisable()
    {
        DynamicMenuManagerContext.Instance.UnregisterMenu(EMenu.FPVMenu, this);
    }
    #endregion
}

 
 */