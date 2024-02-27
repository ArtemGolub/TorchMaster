using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item", order = 1)]
public class ItemSO : ScriptableObject
{
    public string Name;
    public int iD;

    public Transform prefab;

    [Header("Behaviour Types")] 
    public ItemType itemType;
    public FSMType fsmType;

    [Header("Settings")] 
    public float lifeTime;
}