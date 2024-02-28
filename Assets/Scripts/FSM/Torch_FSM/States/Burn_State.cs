using FSM;
using UnityEngine;

public class Burn_State : State
{
    private Item _item;
    private Transform _transform;
    private float _lifeTime;
    private IInventory _inventory;
    
    public Burn_State(Item item, Transform transform)
    {
        _item = item;
        _lifeTime = _item.LifeTime;
        _transform = transform;
    }

    public void SetInventory(IInventory inventory)
    {
        _inventory = inventory;
    }
    
    public override void Update()
    {
        _lifeTime -= Time.deltaTime;
        Debug.Log(_item.Name + " Currently burning");
        if (_lifeTime <= 0)
        {
            _inventory.RemoveItem(_transform,_item);
        }
    }
    public override void Exit()
    {
        _item.LifeTime = _lifeTime;
    }
}