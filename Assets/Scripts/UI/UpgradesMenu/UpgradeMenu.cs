using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private CharacterSO playerUpgrades;
    [SerializeField] private ItemSO torchUpgrades;
    [SerializeField] private ItemSO trueSightUpgrades;
    
    [SerializeField] private List<UpgradePanel> upgradePanels;
    private Dictionary<UpgradeType, int> upgradeLevels = new Dictionary<UpgradeType, int>();
    private const int ProgressionStep = 2;
    private void Start()
    {
        SetPanels();
    }

    private void SetPanels()
    {
        foreach (UpgradePanel panel in upgradePanels)
        {
            panel.SetPanel();
            upgradeLevels.Add(panel.UpgradeType, panel.countUpgrades);
            panel.OnUpgradeButtonClick += UpgradeButtonClickHandler;
        }
    }
    private void UpgradeButtonClickHandler(UpgradeType type)
    {
        AudioManager.current.PlaySFX(SoundType.ButtonClick);
        if (upgradeLevels.ContainsKey(type))
        {
            upgradeLevels[type]++;
            HandleUpgrade(type, upgradeLevels[type]);
        }
    }
    
    private void HandleUpgrade(UpgradeType type, int level)
    {
       
        switch (type)
        {
            case UpgradeType.TorchTime:
                torchUpgrades.burnTime += 5f;
                
                break;
            case UpgradeType.InventoryCapacity:
                playerUpgrades.TorchCapacity += 1;
                break;
            case UpgradeType.TrueSightValue:
                trueSightUpgrades.trueSightRestore += 25f;
                break;
            case UpgradeType.MadnessValue:
                playerUpgrades.maxMadness += 25f;
                break;
        }
    }
}
