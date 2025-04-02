using UnityEngine.UI;
using UnityEngine;

public class SliderHideTextHoverEffect : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    public void OnPointerEnter()
    {
        _slider.transform.GetComponent<Animator>().SetTrigger("HoverStart");
    }
    public void OnPointerExit()
    {
        _slider.transform.GetComponent<Animator>().SetTrigger("HoverEnd");
    }
    private void Awake()
    {
        _slider.value = 0;
    }
}
