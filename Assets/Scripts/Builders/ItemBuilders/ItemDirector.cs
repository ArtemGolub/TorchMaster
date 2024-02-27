using UnityEngine;

public class ItemDirector
{
    private IItemBuilder _itemBuilder;
    
    public Item CreateItem(Transform transform, ItemSO itemData)
    {
        _itemBuilder = ItemBuilderFabric.ItemBuilder(itemData.itemType);
        
        SetParams(itemData);
        SetBehaviour(transform, itemData);
        
        Item item =  _itemBuilder.GetItem();
        return item;
    }

    private void SetParams(ItemSO itemData)
    {
        _itemBuilder.SetName(itemData.Name);
        _itemBuilder.SetID(itemData.iD);
        
        _itemBuilder.SetLifeTime(itemData.lifeTime);
    }

    private void SetBehaviour(Transform transform, ItemSO itemData)
    {
        _itemBuilder.SetFSM(transform, itemData.fsmType);
    }
    
}