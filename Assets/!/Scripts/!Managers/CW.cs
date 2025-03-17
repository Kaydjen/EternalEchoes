using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CW : MonoBehaviour
{
    public static CW I { get; private set; }
    [SerializeField] private GameObject _tabPrefab;
    [SerializeField] private RectTransform _consoleTransform;
    [SerializeField] private float _textSize = 14f;
    [SerializeField] private byte _columnsCount = 5;
    [SerializeField] private byte _consoleMaxWidth = 200;

    private List<TMP_Text> _tabsText;

    private void Start()
    {
        I = this;
        SetUpConsole();
    }

    private void SetUpConsole()
    {
        _tabPrefab.GetComponent<TMP_Text>().fontSize = _textSize; // Set text size

        _consoleTransform.sizeDelta = new Vector2(_consoleMaxWidth, _columnsCount * _textSize); // Set console size

        // add new tabs of text
        _tabsText = new List<TMP_Text>(_columnsCount);
        int existingTabsCount = _consoleTransform.childCount;

        for (int i = existingTabsCount; i < _columnsCount; i++)
        {
            _tabsText.Add(Instantiate(_tabPrefab, _consoleTransform).GetComponent<TMP_Text>());
        }
    }

    public void Print(string text, int time = 1)
    {
        int lastIndex = _columnsCount - 1;
        _tabsText[lastIndex].text = text;
        _tabsText[lastIndex].transform.SetAsFirstSibling();

        UpdateTabsTextOrder();

        StartCoroutine(FadeOutText(time, _tabsText[0]));
    }

    private IEnumerator FadeOutText(int time, TMP_Text text)
    {
        yield return new WaitForSeconds(time);

        float alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime;
            text.alpha = alpha;
            yield return null;
        }

        text.text = string.Empty;
        text.alpha = 1;
    }

    private void UpdateTabsTextOrder()
    {
        _tabsText.Clear();
        foreach (Transform child in _consoleTransform)
        {
            _tabsText.Add(child.GetComponent<TMP_Text>());
        }
    }
}






/*
 
     private void Swap<T>(List<T> list, int index1, int index2)
    {
        (list[index1], list[index2]) = (list[index2], list[index1]);
    }
 
 
 
 */