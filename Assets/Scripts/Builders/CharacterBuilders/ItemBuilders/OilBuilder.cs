using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilBuilder: IItemBuilder
{
    private Item _item = new Item();
        
    public Item GetItem()
    {
        return _item;
    }

    public void SetName(string name)
    {
        _item.Name = name;
    }
        
    public void SetItemType(ItemType type)
    {
        _item.ItemType = type;
    }

    public void SetTransform(Transform transform)
    {
        _item.Transform = transform;
    }

    public void SetCollider(Collider collider)
    {
        _item.Collider = collider;
    }

    public void SetItemCommandManager(ItemSO itemSo)
    {
        
    }

    public void SetItemCommandManager()
    {
        _item.ItemCommandManager = new ItemCommandManager();
        SetBurnCommand();
    }
        
    private void SetBurnCommand()
    {
        //var burnCommand = new BurnStrategy(_item, 2f);
        _item.ItemCommandManager.AddCommand(ItemCommandType.Active, null);
    }

    public void SetFSM(FSMType type)
    {
        _item.FSM = FSMFactory.CreateItemStrategy(_item, type);
    }

    public void SetLightPoints(List<Transform> lightPoints)
    {
        throw new System.NotImplementedException();
    }
    public void SetCollectSound(AudioSource sound)
    {
        _item.collectSound = sound;
    }
    public void SetCantCollectSound(AudioSource sound)
    {
        _item.cantCollectSound = sound;
    }
}
