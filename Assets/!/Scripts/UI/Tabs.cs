using UnityEngine;
using System;

public class Tabs : MonoBehaviour
{
    private const int MAX_TABS = 8;

    [SerializeField] private Tab[] _tabs = new Tab[MAX_TABS];
    private Action[] _methods;

    public void CreateTabs(InteractScriptableObject data)
    {
        if (data == null || data.IconsMain == null || data.IconsHighlighted == null)
        {
            Debug.LogError("Data or icons arrays are null");
            return;
        }
        if (data.IconsMain.Length != data.IconsHighlighted.Length)
        {
            Debug.LogError("Icons arrays have different lengths");
            return;
        }

        int countLimiter = Math.Clamp(data.IconsMain.Length, 0, MAX_TABS);

        for (int i = 0; i < countLimiter; i++)
        {
            if (_tabs[i] == null) continue;

            _tabs[i].MainIcon.sprite = data.IconsMain[i];
            _tabs[i].MainIconHighlighted.sprite = data.IconsHighlighted[i];
            _tabs[i].TemporaryIcon.gameObject.SetActive(false);
            _tabs[i].MainIcon.gameObject.SetActive(true);

            int index = i;
            _tabs[i].Button.onClick.RemoveAllListeners();
            _tabs[i].Button.onClick.AddListener(() => _methods[index]());
        }

        for (int i = countLimiter; i < MAX_TABS; i++)
        {
            if (_tabs[i] == null) continue;

            _tabs[i].TemporaryIcon.gameObject.SetActive(true);
            _tabs[i].MainIcon.gameObject.SetActive(false);
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