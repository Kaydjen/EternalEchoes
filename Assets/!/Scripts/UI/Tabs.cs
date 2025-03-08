using UnityEngine;

public class Tabs : MonoBehaviour
{
    [SerializeField] private RectTransform _tabsPosition;
    [SerializeField] private GameObject _tabPref;
    [SerializeField] private InteractScriptableObject _data;
    private GameObject _instant;
    private void Start()
    {
        CreateTabs(_data);
    }
    public void CreateTabs(InteractScriptableObject data)
    {
        _tabsPosition.sizeDelta = new Vector2(_tabsPosition.sizeDelta.x, 6f * 60f);
        for (int i = 0; i < data.Description.Length; i++)
        {
            _instant = Instantiate(_tabPref, _tabsPosition);
            _instant.transform.SetParent(_tabsPosition);
            _instant.transform.GetComponent<Tab>().Icon.sprite = data.Icons[i];
            _instant.transform.GetComponent<Tab>().Description.text = data.Description[i];
        }
    }

}
