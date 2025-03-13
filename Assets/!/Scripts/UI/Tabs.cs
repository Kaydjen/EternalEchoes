using UnityEngine;
using System;

public class Tabs : MonoBehaviour
{
    [SerializeField] private RectTransform _tabsPosition;
    [SerializeField] private GameObject _tabPref;
    [SerializeField] private InteractScriptableObject _data;
    private Action[] _methods;
    private Transform[] _instants = new Transform[10];
    public void CreateTabs(InteractScriptableObject data)
    {
        _tabsPosition.sizeDelta = new Vector2(_tabsPosition.sizeDelta.x, data.Description.Length * 60f);
        Tab tab;
        for (int i = 0; i < data.Description.Length; i++)
        {
            _instants[i].gameObject.SetActive(true);
            tab = _instants[i].GetComponent<Tab>();

            tab.Icon.sprite = data.Icons[i];
            tab.Description.text = data.Description[i];

            int index = i;
            tab.Button.onClick.RemoveAllListeners();
            tab.Button.onClick.AddListener(() => _methods[index]());
        }
        if(data.Description.Length < _instants.Length)
        {
            for (int i = data.Description.Length; i < _instants.Length; i++)
            {
                _instants[i].gameObject.SetActive(false);
            }
        }
    }
    public void InitButtons(Action[] value)
    {
        _methods = value;
        int index = 0;
        if (_tabsPosition.childCount > 0) index = _tabsPosition.childCount;
        for (; index < 10; index++)
        {
            _instants[index] = Instantiate(_tabPref, _tabsPosition).transform;
        }
    }
}


/*
 
 
     public void CreateTabs(InteractScriptableObject data)
    {
        _tabsPosition.sizeDelta = new Vector2(_tabsPosition.sizeDelta.x, data.Description.Length * 60f);
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
            int index = i;
            tab.Button.onClick.RemoveAllListeners();
            tab.Button.onClick.AddListener(() => _methods[index]());
        }
    }
 
 
 */