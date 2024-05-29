using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private UpgradePanelSO preset;
    [SerializeField] private Image upgrateImage;
    [SerializeField] private TextMeshProUGUI priceText;
    public int countUpgrades;
    public int price;
    [SerializeField] private List<Image> upgrates;
    [SerializeField] private Button btnUpgrade;
    public UpgradeType UpgradeType;
    public event Action<UpgradeType> OnUpgradeButtonClick;
    
    public void SetPanel()
    {
        upgrateImage.sprite = preset.upgrateImage;
        countUpgrades = preset.countUpgrades;
        UpgradeType = preset.UpgradeType;
        price = preset.price;
        priceText.text = price.ToString();
        CountUpgrades();
        CheckUpdateEnabled();

        btnUpgrade.onClick.AddListener(() => UpgradeButtonClick());
    }
    private void UpgradeButtonClick()
    {
        if (OnUpgradeButtonClick != null)
        {
            if(!CurrencyManager.current.RemoveCurrency(price)) return;
            Upgrade();
        }
    }

    private void Upgrade()
    {
        preset.countUpgrades++;
        countUpgrades++;
        preset.price += 50 * countUpgrades;
        CountUpgrades();
        OnUpgradeButtonClick.Invoke(UpgradeType);
        CheckUpdateEnabled();
        price = preset.price;
        priceText.text = price.ToString();
    }

    private void CheckUpdateEnabled()
    {
        if (countUpgrades >= upgrates.Count)
        {
            btnUpgrade.GetComponent<Image>().enabled = false;
            btnUpgrade.enabled = false;
        }
    }
    private void CountUpgrades()
    {
        for (int i = 0; i < countUpgrades; i++)
        {
            upgrates[i].GetComponent<Image>().color = Color.green;
        }
    }
    
}
