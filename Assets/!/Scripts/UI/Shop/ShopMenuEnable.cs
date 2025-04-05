using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ShopMenuEnable : MonoBehaviour
{
    [SerializeField] private Transform _canvas;
    public void DisableMenu()
    {
        _canvas.gameObject.SetActive(false);
    }
    public void EnableMenu()
    {
        _canvas.gameObject.SetActive(true);
    }
}
public class ShopMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private int _money;
    public static UnityEvent OnMoneyUpdate = new();
    public int Money
    {
        get => _money; 
        protected set
        {
            _money = value;
            _moneyText.text = _money.ToString();
            OnMoneyUpdate?.Invoke();
        }
    }
    public void GetMoneyFromCharacter()
    {
        Money += PlayerCore.Instance.GetComponent<HPRecovery>().GetAllSouls();
    }
}
