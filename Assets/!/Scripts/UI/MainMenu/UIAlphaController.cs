using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAlphaController : MonoBehaviour
{
    public Image Icon;
    public TMP_Text Text;
    private Color _colorMain = new Color();
    private Color _colorText = new Color();
    private void Awake()
    {
        _colorMain = Icon.color;
        _colorText = Text.color;
    }
    public void ChangeAlpha(float value)
    {
        _colorMain.a = value;
        _colorText.a = value;
        Icon.color = _colorMain;
        Text.color = _colorText;
    }
}
