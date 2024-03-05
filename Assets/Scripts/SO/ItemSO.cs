using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item", order = 1)]
public class ItemSO : ScriptableObject
{
    public string Name;

    public Transform prefab;

    [Header("Behaviour Types")] 
    public ItemType itemType;
    public FSMType fsmType;
}