using UnityEngine;

public sealed class Torch : MonoBehaviour
{
    [SerializeField] private ItemSO itemPreset;
    public Item item;
    
    private void Awake()
    {
        InitItem();
    }

    private void Start()
    {
        item.SM.InitBehaviour();
    }

    private void Update()
    {
        item.SM.UpdateBehaviour();
    }

    private void InitItem()
    {
        item = ItemBuilder.current.CreateItem(transform, itemPreset);
    }

    private void OnCollisionEnter(Collision other)
    {
        var player = other.transform.GetComponent<Player>();
        if (!player) return;
        player.Character.Inventory.GrabItem(transform, item);
    }
}