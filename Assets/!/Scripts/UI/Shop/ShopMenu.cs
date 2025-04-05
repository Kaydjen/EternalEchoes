using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private Transform _canvas;
    public void DisableMenu()
    {
        _canvas.gameObject.SetActive(false);
    }
    public void EnableMenu()
    {
        _canvas.gameObject.SetActive(true);
    }
}
