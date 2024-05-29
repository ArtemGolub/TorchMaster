using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item", order = 1)]
public class ItemSO : ScriptableObject
{
    public string Name;

    public Transform prefab;
    public float burnTime;
    public float trueSightRestore;
    
    [Header("Sounds")]
    public AudioSource soundSource;
    public AudioSource cantCollectSoundSource;
    
    [Header("Behaviour Types")] 
    public ItemType itemType;
    public FSMType fsmType;
}