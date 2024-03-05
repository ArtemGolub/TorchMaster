using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class AItem : MonoBehaviour, IItem
{
    [SerializeField] private ItemSO itemPreset;
    public Item item { get; set; }
    private Collider collider;

    protected void InitItem()
    {
        collider = GetComponent<Collider>();
        item = ItemsBuilder.current.CreateItem(transform, itemPreset, collider);
    }
}