using UnityEngine;

public class InteractOptions : MonoBehaviour
{
    #region VARIABLES
    public static InteractOptions Instance { get; private set; }
    [SerializeField] private GameObject _manu;
    private IInteractStrategy _context;
    #endregion
    #region PUBLIC METHODS
    public void EnableManu(IInteractStrategy strategy)
    {
        InputHandler.Instance.ActivateOptionsNumbersMap();
        _context = strategy;

        GetComponent<Tabs>().CreateTabs(_context.GetData());

        CameraSwitcher.Instance.DisableCurrentView();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        AIPlSwapper.DisReabilitation();

        _manu.SetActive(true);
    }
    public void DisableManu()
    {
        InputHandler.Instance.ActivateDefNumbersMap();
        CameraSwitcher.Instance.EnableCurrentView();
        AIPlSwapper.Reabilitation();
        _manu.SetActive(false);
    }
    #endregion
    #region PRIVATE METHODS 
    public void Action1() // TODO: тут мб класс надо будет переделать (вызовы ExecuteAlgorithm1)
    {
        _context.Action1();
        DisableManu();
    }
    public void Action2()
    {
        _context.Action2();
        DisableManu();
    }
    public void Action3()
    {
        _context.Action3();
        DisableManu();
    }
    public void Action4()
    {
        _context.Action4();
        DisableManu();
    }
    public void Action5()
    {
        _context.Action5();
        DisableManu();
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
    #endregion
}
