using System.Collections;
using UnityEngine;

public class Tabs : MonoBehaviour
{
    [SerializeField] private Transform _tabsPosition;
    [SerializeField] private GameObject _tabPref;
    private GameObject _instant;
    private void Start()
    {
        StartCoroutine(nameof(SetTabs));
    }
    public IEnumerator SetTabs()
    {
        for (int i = 0; i < 5; i++)
        {
            _instant = Instantiate(_tabPref, _tabsPosition);
            _instant.transform.SetParent(_tabsPosition);
            yield return new WaitForEndOfFrame();
        }
    }
}
