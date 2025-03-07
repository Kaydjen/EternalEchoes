using UnityEngine;

public class InteractOptions : MonoBehaviour
{
    #region VARIABLES
    private static GameObject _manu;
    private static IInteractStrategy _context;
    private static InteractScriptableObject _data;
    #endregion
    #region PUBLIC METHODS
    public static void EnableManu(IInteractStrategy strategy)
    {
        InputHandler.Instance.ActivateOptionsNumbersMap();

        _context = strategy;
        _data = _context.GetData();
        _manu.SetActive(true);
    }
    public static void DisableManu()
    {
        InputHandler.Instance.ActivateDefNumbersMap();
        _manu.SetActive(false);
    }
    #endregion
    #region PRIVATE METHODS 
    private void Action1() // TODO: тут мб класс надо будет переделать (вызовы ExecuteAlgorithm1)
    {
        _context.Action1();
        DisableManu();
    }
    private void Action2()
    {
        _context.Action2();
        DisableManu();
    }
    private void Action3()
    {
        _context.Action3();
        DisableManu();
    }
    private void Action4()
    {
        _context.Action4();
        DisableManu();
    }
    private void Action5()
    {
        _context.Action5();
        DisableManu();
    }
    #endregion
    #region MONO METHODS
    public void Init()
    {
        InputHandler.OnOptionsOne.AddListener(Action1);
        InputHandler.OnOptionsTwo.AddListener(Action2);
        InputHandler.OnOptionsThree.AddListener(Action3);
        InputHandler.OnOptionsFour.AddListener(Action4);
        InputHandler.OnOptionsFive.AddListener(Action5);
        
        _manu = this.transform.GetChild(0).transform.gameObject;
    }
    #endregion
}
