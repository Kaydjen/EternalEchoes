using UnityEngine;
using System;

public class Tabs : MonoBehaviour
{
    [SerializeField] private InteractScriptableObject _data;
    [SerializeField] private Tab[] _tabs = new Tab[8];
    private Action[] _methods;
    public void CreateTabs(InteractScriptableObject data)
    {
        int countLimiter = Math.Clamp(_data.IconsMain.Length, 0, 8);
        if (countLimiter == 0) return;
        for (int i = 0; i < countLimiter; i++)
        {
            _tabs[i].MainIcon.sprite = _data.IconsMain[i];
            _tabs[i].MainIconHighlighted.sprite = _data.IconsHighlighted[i];
            //_tabs[i].MainIconPressed.sprite = _data.IconsPressed[i];
            _tabs[i].TemporaryIcon.gameObject.SetActive(false);

            int index = i;
            _tabs[i].Button.onClick.RemoveAllListeners();
            _tabs[i].Button.onClick.AddListener(() => _methods[index]());
            //_tabs[i].Button.onClick.AddListener(() => _tabs[i].MainIconPressed.gameObject.SetActive(true));
        }
        if(countLimiter == 8) return;
        for (int i = countLimiter-1; i < 8; i++)
        {
            _tabs[i].TemporaryIcon.gameObject.SetActive(true);
            _tabs[i].MainIcon.gameObject.SetActive(false);
            //_tabs[i].MainIconHighlighted.gameObject.SetActive(false);
            //_tabs[i].MainIconPressed.gameObject.SetActive(false);
        }
    }
    public void InitButtonMethods(Action[] value)
    {
        _methods = value;
    }
}



















/*
 
 public class Tabs : MonoBehaviour
{
    [SerializeField] private RectTransform _tabsPosition;
    [SerializeField] private GameObject _tabPref;
    [SerializeField] private InteractScriptableObject _data;
    [SerializeField] private byte _initTabsCount = 5;
    private Action[] _methods;
    private Transform[] _instants;
    public void CreateTabs(InteractScriptableObject data)
    {
        _tabsPosition.sizeDelta = new Vector2(_tabsPosition.sizeDelta.x, data.Description.Length * 60f);
        Tab tab;
        for (int i = 0; i < data.Description.Length; i++)
        {
            _instants[i].gameObject.SetActive(true);
            tab = _instants[i].GetComponent<Tab>();

            tab.MainIcon.sprite = data.IconsMain[i];
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
    public void InitButtonMethods(Action[] value)
    {
        _methods = value;
        int index = 0;
        if (_tabsPosition.childCount > 0) index = _tabsPosition.childCount;
        _instants = new Transform[_initTabsCount];
        for (; index < _initTabsCount; index++)
        {
            _instants[index] = Instantiate(_tabPref, _tabsPosition).transform;
        }
    }
}

 
 */
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
            tab.MainIcon.sprite = data.IconsMain[i];
            tab.Description.text = data.Description[i];
            int index = i;
            tab.Button.onClick.RemoveAllListeners();
            tab.Button.onClick.AddListener(() => _methods[index]());
        }
    }
 
 
 */