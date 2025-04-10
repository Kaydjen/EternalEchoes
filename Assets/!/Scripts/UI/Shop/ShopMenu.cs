using TMPro;
using UnityEngine;
using UnityEngine.Events;

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
        Money += PlayerCore.Instance.GetComponent<SoulsBank>().GetAllSouls();
    }
}
