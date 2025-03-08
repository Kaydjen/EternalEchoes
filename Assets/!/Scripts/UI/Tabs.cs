using UnityEngine;

public class Tabs : MonoBehaviour
{
    [SerializeField] private RectTransform _tabsPosition;
    [SerializeField] private GameObject _tabPref;
    private GameObject _instant;
/*    private void Start()
    {
        StartCoroutine(nameof(SetTabs));
    }*/
    public void CreateTabs(InteractScriptableObject data)
    {
        _tabsPosition.sizeDelta = new Vector2(_tabsPosition.sizeDelta.x, 6f * 60f);
        for (int i = 0; i < 6; i++)
        {
            _instant = Instantiate(_tabPref, _tabsPosition);
            _instant.transform.SetParent(_tabsPosition);
        }
    }
/*    public IEnumerator SetTabs()
    {
        _tabsPosition.sizeDelta = new Vector2(_tabsPosition.sizeDelta.x, 6f * 60f);
        for (int i = 0; i < 6; i++)
        {
            _instant = Instantiate(_tabPref, _tabsPosition);
            _instant.transform.SetParent(_tabsPosition);
        }
        yield return null;
    }*/
}
