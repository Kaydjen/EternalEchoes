using UnityEngine;

public class ConcreteSwitchCharacter : MonoBehaviour, IInteractStrategy
{
    [SerializeField] private InteractScriptableObject _data;
    public InteractScriptableObject GetData()
    {
        return _data;
    }
    public void Action1()
    {
        this.transform.GetComponent<PlayerCore>().enabled = true;
    }

    public void Action2()
    {

    }

    public void Action3()
    {

    }

    public void Action4()
    {

    }

    public void Action5()
    {

    }
}
