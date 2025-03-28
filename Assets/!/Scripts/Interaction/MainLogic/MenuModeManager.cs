using UnityEngine;

public class MenuModeManager : MonoBehaviour
{
    public static EMenu MenuType { get; private set; }

    private void Start() => MenuType = EMenu.FPVMenu;
    private void OnEnable()
    {
        CameraSwitcher.OnFPV_Enable.AddListener(() => MenuType = EMenu.FPVMenu);
        CameraSwitcher.OnIsometricV_Enable.AddListener(() => MenuType = EMenu.RTSMenu);
    }
    private void OnDisable()
    {
        CameraSwitcher.OnFPV_Enable.RemoveListener(() => MenuType = EMenu.FPVMenu);
        CameraSwitcher.OnIsometricV_Enable.RemoveListener(() => MenuType = EMenu.RTSMenu);
    }
}






















/*
 
             Vector3 size = hit.transform.position - _zona.position; // Get the difference

            // Set the scale based on distance
            _zona.localScale = new Vector3(size.x, 3f, size.z);

            // Adjust position to keep A corner fixed
            _zona.position = _zona.position + new Vector3(size.x / 2, 0, size.z / 2);
 
 */