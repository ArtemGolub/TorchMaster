using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters/Character", order = 1)]
public class CharacterSO : ScriptableObject
{
    public string Name;
    public int iD;

    public Transform prefab;
    
    [Header("Behaviour Types")]
    public CharacterType characterType;
    public MovementType movementType;
    public FSMType fsmType;
    public InventoryType inventoryType;

    [Header("Settings")] 
    public float speed;
}