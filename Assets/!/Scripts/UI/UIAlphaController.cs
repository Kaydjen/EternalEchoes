using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAlphaController : MonoBehaviour
{
    public Image Main;
    public Image Highlighted;
    public Image Pressed;
    public TMP_Text Text;
    private Color _colorMain = new Color();
    private Color _colorHighlighted = new Color();
    private Color _colorPressed = new Color();
    private Color _colorText = new Color();
    private void Awake()
    {
        _colorMain = Main.color;
        _colorHighlighted = Highlighted.color;
        _colorPressed = Pressed.color;
        _colorText = Text.color;
    }
    public void ChangeAlpha(float value)
    {
        _colorMain.a = value;
        _colorText.a = value;
        Main.color = _colorMain;
        Text.color = _colorText;
    }
}
