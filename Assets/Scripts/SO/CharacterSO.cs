using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters/Character", order = 1)]
public class CharacterSO : ScriptableObject
{
    public string Name;
    
    public Transform prefab;
    public AudioSource attackAudio;
    
    [Header("Behaviour Types")]
    public CharacterType characterType;
    public MovementType movementType;
    public FSMType fsmType;
    public InventoryType inventoryType;
    public AmmoType ammoType;
    public AttackType attackType;
    
    [Header("Settings")] 
    public float speed;
    public float attackRange;
    public float raloadTime;
    public int TorchCapacity;
    public float maxMadness;
    
    [Header("Collision Tags")]
    public CollisionTags[] collisionTags;
}