using UnityEngine;
using System;

public class Tabs : MonoBehaviour
{
    [SerializeField] private RectTransform _tabsPosition;
    [SerializeField] private GameObject _tabPref;
    [SerializeField] private InteractScriptableObject _data;
    private Action[] _methods;
    private Transform _instant;
    public void CreateTabs(InteractScriptableObject data)
    {
        _tabsPosition.sizeDelta = new Vector2(_tabsPosition.sizeDelta.x, 6f * 60f);
        Tab tab;
        for (int i = 0; i < data.Description.Length; i++)
        {
            if (_tabsPosition.childCount < i+1)
            {
                _instant = Instantiate(_tabPref, _tabsPosition).transform;
            }
            else
            {
                _instant = _tabsPosition.GetChild(i);
            }
            tab = _instant.GetComponent<Tab>();
            tab.Icon.sprite = data.Icons[i];
            tab.Description.text = data.Description[i];
            tab.Button.onClick.AddListener(() => _methods[i]());
        }
    }
    public void InitButtons(Action[] methods)
    {
        _methods = new Action[] { methods[0], methods[1], methods[2], methods[3], methods[4]};
    }
}
