using System.Collections.Generic;
using UnityEngine;

public static class DynamicMenuManagerContext
{
    private static Dictionary<EMenu, List<IMenu>> _menuList = new();

    public static void EnableMenu(EMenu type)
    {
        if (_menuList.ContainsKey(type))
        {
            foreach (IMenu item in _menuList[type])
            {
                item.EnableMenu();
            }
        }
        else
        {
            Debug.Log($"{nameof(_menuList)} doesn't contain a key of type {nameof(type)} in method {nameof(EnableMenu)} (in script {nameof(DynamicMenuManagerContext)}");
        }
    }
    public static void DisableMenu(EMenu type)
    {
        if (_menuList.ContainsKey(type))
        {
            foreach (IMenu item in _menuList[type])
            {
                item.DisableMenu();
            }
        }
        else
        {
            Debug.Log($"{nameof(_menuList)} doesn't contain a key of type {nameof(type)} in method {nameof(DisableMenu)} (in script {nameof(DynamicMenuManagerContext)}");
        }
    }
    public static void RegisterMenu(EMenu menuType, IMenu logic) 
    {
        if (!_menuList.ContainsKey(menuType))        
            _menuList.Add(menuType, new List<IMenu>());

        _menuList[menuType].Add(logic);
    }
    public static void UnregisterMenu(EMenu menuType, IMenu logic = null)
    {
        if (!_menuList.ContainsKey(menuType))
        {
            Debug.Log($"{nameof(_menuList)} doesn't contain a key of type {nameof(menuType)} in method {nameof(UnregisterMenu)} (in script {nameof(DynamicMenuManagerContext)}");
            return;
        }
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
}





























/*
 
             Vector3 size = hit.transform.position - _zona.position; // Get the difference

            // Set the scale based on distance
            _zona.localScale = new Vector3(size.x, 3f, size.z);

            // Adjust position to keep A corner fixed
            _zona.position = _zona.position + new Vector3(size.x / 2, 0, size.z / 2);
 
 */