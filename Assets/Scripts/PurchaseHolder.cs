using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseHolder : MonoBehaviour
{
    [SerializeField] private int currencyAmount;
    [SerializeField] private TextMeshProUGUI amountRecive;
    [SerializeField] private Button btnPurchase;

    private void Start()
    {
        btnPurchase.onClick.AddListener(Purchase);
        amountRecive.text = currencyAmount.ToString();
    }

    private void Purchase()
    {
        AudioManager.current.PlaySFX(SoundType.ButtonClick);
        CurrencyManager.current.AddCurrency(currencyAmount);
    }
}
