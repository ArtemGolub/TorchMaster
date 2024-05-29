using System.Collections.Generic;
using UnityEngine;

public class ItemDirector
{
    private IItemBuilder _itemBuilder;
    
    public Item CreateItem(Transform transform, ItemSO itemData, Collider collider, List<Transform> lightPoints)
    {
        _itemBuilder = ItemBuilderFabric.ItemBuilder(itemData.itemType);
        
        SetParams(itemData, transform, collider, lightPoints);
        
        Item item =  _itemBuilder.GetItem();
        return item;
    }

    private void SetParams(ItemSO itemData, Transform transform, Collider collider, List<Transform> lightPoints)
    {
        _itemBuilder.SetLightPoints(lightPoints);
        
        _itemBuilder.SetName(itemData.Name);
        _itemBuilder.SetItemType(itemData.itemType);
        _itemBuilder.SetTransform(transform);
        _itemBuilder.SetCollider(collider);
        
        _itemBuilder.SetItemCommandManager(itemData);
        
        _itemBuilder.SetFSM(itemData.fsmType);
        
        _itemBuilder.SetCollectSound(itemData.soundSource);
        _itemBuilder.SetCantCollectSound(itemData.cantCollectSoundSource);
    }
    
}