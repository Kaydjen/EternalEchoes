using UnityEngine;

public class InteractOptions : MonoBehaviour
{
    #region VARIABLES
    private static GameObject _manu;
    private static InteractStrategyContext _context = new (new ConcreteGreetings());
    #endregion
    #region PUBLIC METHODS
    public static void EnableManu(IInteractStrategy strategy)
    {
        InputHandler.Instance.ActivateOptionsNumbersMap();

        _context.ContextStrategy = strategy;

        _manu.SetActive(true);
    }
    public static void DisableManu()
    {
        InputHandler.Instance.ActivateDefNumbersMap();
        _manu.SetActive(false);
    }
    #endregion
    #region PRIVATE METHODS
    private void Action1()
    {
        _context.ExecuteAlgorithm1();
        //DisableManu();
    }
    private void Action2()
    {
        _context.ExecuteAlgorithm2();
        //DisableManu();
    }
    private void Action3()
    {
        _context.ExecuteAlgorithm3();
        //DisableManu();
    }
    private void Action4()
    {
        _context.ExecuteAlgorithm4();
        //DisableManu();
    }
    private void Action5()
    {
        _context.ExecuteAlgorithm5();
        //DisableManu();
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
