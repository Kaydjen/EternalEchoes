using UnityEngine;

public class OBJRTS: OBJOptionsManu
{
    #region PUBLIC
    public override void EnableMenu()
    {
        base.EnableMenu();
        InputHandler.Instance.ActivateOptionsNumbersMap();
        DisableComponents();
    }
    public override void DisableMenu()
    {
        base.DisableMenu();
        InputHandler.Instance.ActivateDefNumbersMap();
        ActivateComponents();
    }
    #endregion
    #region PROTECTED
    protected virtual void DisableComponents()
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
    protected virtual void ActivateComponents()
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
    #region MONOBEHAVIOUR
    protected virtual void OnEnable()
    {
        DynamicMenuManagerContext.Instance.RegisterMenu(EMenu.RTSMenu, this);

        InputHandler.OnOptionsOne.AddListener  (ExecuteAction1);
        InputHandler.OnOptionsTwo.AddListener  (ExecuteAction2);
        InputHandler.OnOptionsThree.AddListener(ExecuteAction3);
        InputHandler.OnOptionsFour.AddListener (ExecuteAction4);
        InputHandler.OnOptionsFive.AddListener (ExecuteAction5);
    }
    protected virtual void OnDisable()
    {
        DynamicMenuManagerContext.Instance.UnregisterMenu(EMenu.RTSMenu, this);
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