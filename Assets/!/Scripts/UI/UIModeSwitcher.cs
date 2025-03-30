using UnityEngine;

public class UIModeSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _FPVMenu;
    [SerializeField] private GameObject _IsometricMenu;
    [SerializeField] private GameObject _TopDownMenu;
    private void EnableFPVMenu() 
    {
        _FPVMenu.SetActive(true);
        DisableAll();
    }
    private void EnableIsometricMenu()
    {
        _IsometricMenu.SetActive(true);
        DisableAll();
    }   
    private void Enable_TopDownMenu()
    {
        _TopDownMenu.SetActive(true);
        DisableAll();
    }
    private void DisableAll()
    {
        _FPVMenu.SetActive(false);
        _IsometricMenu.SetActive(false);
        _TopDownMenu.SetActive(false);
    }
    private void Start()
    {
        CameraSwitcher.OnFPV_Enable.AddListener(EnableFPVMenu);
        CameraSwitcher.OnIsometricV_Enable.AddListener(EnableIsometricMenu);
        CameraSwitcher.OnTopDownV_Enable.AddListener(Enable_TopDownMenu);
    }
}