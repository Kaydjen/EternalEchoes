using System.Collections;
using TMPro;
using UnityEngine;

public class CW : MonoBehaviour
{
    public static CW I;
    [SerializeField] private TMP_Text _textField;
    public void Print(string text)
    {
        _textField.text = text;
        StartCoroutine(Delay(3));
    }
    public void Print(string text, int time)
    {
        _textField.text = text;
        StartCoroutine(Delay(time));
    }
    private IEnumerator Delay(int time)
    {
        yield return new WaitForSeconds(time);
        float alpha = 1; 
        while (alpha > 0)
        {
            alpha -= Time.deltaTime;
            _textField.alpha = alpha;
            yield return null;
        }
        _textField.text = "";
        _textField.alpha = 1;
    }
    private void Start()
    {
        I = this;
    }
}