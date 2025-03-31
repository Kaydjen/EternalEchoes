using UnityEngine.EventSystems;
using UnityEngine;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Курсор на кнопке!");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Курсор ушёл с кнопки!");
    }
}