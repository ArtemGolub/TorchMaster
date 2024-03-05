using UnityEngine;

public sealed class Torch : MonoBehaviour
{
    [SerializeField] private ItemSO itemPreset;
    public Item item { get; set; }
    public Collider collider;
    private bool isCollected;
    private void Awake()
    {
        InitItem();
    }
    
    private void InitItem()
    {
        item = ItemsBuilder.current.CreateItem(transform, itemPreset, collider);
    }
}