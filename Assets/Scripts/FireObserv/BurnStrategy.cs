using System.Collections.Generic;
using UnityEngine;

public class BurnStrategy : IBurnStategy, IStrategy
{
    private Item _item;
    private float _burnTime;
    
    // TODO refactor lightPoints
    private List<Transform> lightPoint;
    
    public BurnStrategy(Item item, float burnTime)
    {
        _item = item;
        _burnTime = burnTime;
        
        lightPoint = _item.LightPoint;
    }
    public void Burn()
    {
        _burnTime -= Time.deltaTime;
        TorchCanvas.current.UpdateSlider(_burnTime);
        if (_burnTime <= 0)
        {
            _item.FSM.ChangeState(ItemStateType.Used);
        }
    }

    public void Subscribe()
    {
        foreach (var point in lightPoint)
        {
            point.gameObject.SetActive(true);
        }
        BurnObserver.current.AddObserver(this);
    }

    public void UnSubscribe()
    {
        foreach (var point in lightPoint)
        {
            point.gameObject.SetActive(false);
        }
        BurnObserver.current.RemoveObserver(this);
    }
}
