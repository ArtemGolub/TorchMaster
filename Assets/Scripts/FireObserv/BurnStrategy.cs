using UnityEngine;

public class BurnStrategy : IBurnStategy, IStrategy
{
    private Item _item;
    private float _burnTime;
    
    public BurnStrategy(Item item, float burnTime)
    {
        _item = item;
        _burnTime = burnTime;
    }
    public void Burn()
    {
        _burnTime -= Time.deltaTime;
        if (_burnTime <= 0)
        {
            _item.FSM.ChangeState(ItemStateType.Used);
        }
    }

    public void Subscribe()
    {
        BurnObserver.current.AddObserver(this);
    }

    public void UnSubscribe()
    {
        BurnObserver.current.RemoveObserver(this);
    }
}
