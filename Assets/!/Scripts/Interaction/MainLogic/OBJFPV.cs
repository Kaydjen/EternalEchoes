public class OBJFPV : OBJOptionsManu
{
    #region PUBLIC
    public override void EnableMenu()
    {
        base.EnableMenu();
        // Enable behaviour of FPV manu 
        GetComponent<RotateMenuOnCharacter>().Enable(PlayerCore.Instance.transform, Hover.HitedCollider.transform);
    }
    public override void DisableMenu()
    {
        base.DisableMenu();
    }

    #endregion
    #region MONOBEHAVIOUR
    protected void OnEnable()
    {
        DynamicMenuManagerContext.Instance.RegisterMenu(EMenu.FPVMenu, this);
    }
    protected void OnDisable()
    {
        DynamicMenuManagerContext.Instance.UnregisterMenu(EMenu.FPVMenu, this);
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