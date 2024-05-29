using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class AItem : MonoBehaviour, IItem
{
    [SerializeField] private ItemSO itemPreset;
    public Item item { get; set; }
    public List<Transform> lightPoint;
    private Collider collider;

    protected void InitItem()
    {
        collider = GetComponent<Collider>();
        item = ItemsBuilder.current.CreateItem(transform, itemPreset, collider, lightPoint);
        item.TrueSightRestore = itemPreset.trueSightRestore;
    }
}