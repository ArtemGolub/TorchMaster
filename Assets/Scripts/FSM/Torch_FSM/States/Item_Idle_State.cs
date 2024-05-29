using FSM;
using UnityEngine;

public class Item_Idle_State : State
{
    private Item _item;

    private float rotateSpeed = 0;
    public Item_Idle_State(Item item)
    {
        _item = item;
    }
    
    
    public override void Update()
    {
        rotateSpeed += 35 * Time.deltaTime;
        _item.Transform.rotation = Quaternion.Euler(0, rotateSpeed,0);
    }
}