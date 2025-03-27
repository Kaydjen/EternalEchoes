using System.Collections.Generic;
using UnityEngine;

public class DynamicMenuManagerContext : MonoBehaviour
{
    public static DynamicMenuManagerContext Instance;
    private Dictionary<EMenu, IMenu> _menuList = new();

    public void EnableMenu(EMenu type)
    {
        if (_menuList.TryGetValue(type, out IMenu menu)) menu?.EnableMenu();
    }
    public void DisableMenu(EMenu type)
    {
        if (_menuList.TryGetValue(type, out IMenu menu)) menu?.EnableMenu();
    }
    public void AddMenu(EMenu menuType, IMenu logic) 
    {
        _menuList.Add(menuType, logic);
    }
    private void Awake()
    {
        Instance = this;
    }
}





























/*
 
             Vector3 size = hit.transform.position - _zona.position; // Get the difference

            // Set the scale based on distance
            _zona.localScale = new Vector3(size.x, 3f, size.z);

            // Adjust position to keep A corner fixed
            _zona.position = _zona.position + new Vector3(size.x / 2, 0, size.z / 2);
 
 */