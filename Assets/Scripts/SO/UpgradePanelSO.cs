using UnityEngine;

[CreateAssetMenu(fileName = "New Item Upgrade", menuName = "Items/ItemUpgrade", order = 2)]
public class UpgradePanelSO : ScriptableObject
{
    public Sprite upgrateImage;
    public int countUpgrades;
    public int price;
    public UpgradeType UpgradeType;
}