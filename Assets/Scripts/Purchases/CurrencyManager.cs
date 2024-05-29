using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager current;
    private int _currency;
    [SerializeField] private TextMeshProUGUI CoinsAmount;
    private void Awake()
    {
        if (current == null)
        {
            current = this;
            UpdateAmount();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void UpdateAmount()
    {
        CoinsAmount.text = _currency.ToString();
    }
    public void AddCurrency(int amount)
    {
        _currency += amount;
        UpdateAmount();
    }

    public bool RemoveCurrency(int amount)
    {
        if ((_currency - amount) < 0)
        {
            Debug.Log("not enough");
            return false;
        }
        Debug.Log("all ok");
        _currency -= amount;
        UpdateAmount();
        return true;
    }
}