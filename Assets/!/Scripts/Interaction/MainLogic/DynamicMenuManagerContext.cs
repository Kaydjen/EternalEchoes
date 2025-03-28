using System;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMenuManagerContext : MonoBehaviour
{
    public static DynamicMenuManagerContext Instance;
    private Dictionary<EMenu, List<IMenu>> _menuList = new();

    public void EnableMenu(EMenu type)
    {
        if (_menuList.ContainsKey(type))
        {
            foreach (IMenu item in _menuList[type])
            {
                item.EnableMenu();
            }
        }
    }
    public void DisableMenu(EMenu type)
    {
        if (_menuList.ContainsKey(type))
        {
            foreach (IMenu item in _menuList[type])
            {
                item.DisableMenu();
            }
        }
    }
    public void RegisterMenu(EMenu menuType, IMenu logic) 
    {
        if (!_menuList.ContainsKey(menuType))        
            _menuList.Add(menuType, new List<IMenu>());

        _menuList[menuType].Add(logic);
    }
    public void UnregisterMenu(EMenu menuType, IMenu logic = null)
    {
        if (!_menuList.ContainsKey(menuType)) return;
        if (logic == null)
        {
            _menuList.Remove(menuType);
        }
        else
        {
            _menuList[menuType].Remove(logic);
            if(_menuList[menuType].Count == 0) _menuList.Remove(menuType);
        }
    }
    public void Init()
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