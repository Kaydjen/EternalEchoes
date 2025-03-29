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
       // GetComponent<Tabs>().CreateTabs(_context.GetData());
    }
    protected virtual void SetTabs()
    {
       // GetComponent<Tabs>().InitButtons(new System.Action[] { ExecuteAction1, ExecuteAction2, ExecuteAction3, ExecuteAction4, ExecuteAction5 });
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
        InputHandler.Instance.ActivateOptionsNumbersMap();
        _manu.SetActive(true);
        DisableComponents();
    }
    public void DisableMenu()
    {
        InputHandler.Instance.ActivateDefNumbersMap();
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
        InputHandler.Instance.SetAllAim(false);
        InputHandler.Instance.SetAllAttack(false);
        InputHandler.Instance.SetAllMovement(false);
        InputHandler.Instance.SetAllInteract(false);
        InputHandler.Instance.SetUIState(true); 

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        CameraSwitcher.Instance.DisableCurrentView();
        AIPlSwapper.LockControl();
    }
    private void ActivateComponents()
    {
        InputHandler.Instance.SetAllAim(true);
        InputHandler.Instance.SetAllAttack(true);
        InputHandler.Instance.SetAllMovement(true);
        InputHandler.Instance.SetAllInteract(true);
        InputHandler.Instance.SetUIState(false);

        if (!CameraSwitcher.Instance.IsCurrentViewEnabled()) CameraSwitcher.Instance.EnableCurrentView();
        AIPlSwapper.UnlockControl();
    }
    #endregion
    #region MONO METHODS
    public void Init()
    {
        Instance = this;

        InputHandler.OnOptionsOne.AddListener(Action1);
        InputHandler.OnOptionsTwo.AddListener(Action2);
        InputHandler.OnOptionsThree.AddListener(Action3);
        InputHandler.OnOptionsFour.AddListener(Action4);
        InputHandler.OnOptionsFive.AddListener(Action5);

        GetComponent<Tabs>().InitButtons(new System.Action[] { Action1, Action2, Action3, Action4, Action5 });
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