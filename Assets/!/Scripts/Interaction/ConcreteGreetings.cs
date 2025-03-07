using UnityEngine;

public class ConcreteGreetings : MonoBehaviour, IInteractStrategy
{
    [SerializeField] private InteractScriptableObject _data;
    public InteractScriptableObject GetData()
    {
        return _data;
    }
    public void Action1()
    {
        Debug.Log("It works");
    }

    public void Action2()
    {
        Debug.Log("Зачем ты нажал на кнопку?");
    }

    public void Action3()
    {
        Debug.Log("О, неужели ты снова нажал эту кнопку? Серьезно?");
    }

    public void Action4()
    {
        Debug.Log("Ну вот, опять! Ты что, на спам подписался?");
    }

    public void Action5()
    {
        Debug.Log("НЕ НАЖИМАЙ");
    }
}
