using UnityEngine;

public class ItemDirector
{
    private IItemBuilder _itemBuilder;
    
    public Item CreateItem(Transform transform, ItemSO itemData, Collider collider)
    {
        _itemBuilder = ItemBuilderFabric.ItemBuilder(itemData.itemType);
        
        SetParams(itemData, transform, collider);
        
        Item item =  _itemBuilder.GetItem();
        return item;
    }

    private void SetParams(ItemSO itemData, Transform transform, Collider collider)
    {
        _itemBuilder.SetName(itemData.Name);
        _itemBuilder.SetItemType(itemData.itemType);
        _itemBuilder.SetTransform(transform);
        _itemBuilder.SetCollider(collider);
        
        _itemBuilder.SetFSM(itemData.fsmType);
    }
    
}