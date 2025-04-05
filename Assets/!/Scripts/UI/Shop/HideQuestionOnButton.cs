using UnityEngine;

public class HideQuestionOnButton : MonoBehaviour
{
    [SerializeField] GameObject Question;
    public void DisableQuestion()
    {
        Question.SetActive(false);
        Destroy(this);
    }
}