using UnityEngine;

public class RuleTest : MonoBehaviour, IRule
{
    public bool CanExecute()
    {
        Debug.Log("CanExecute");
        return true;
    }

    public void Execute()
    {
        Debug.Log("Execute! Za ordu!!");
    }
}
