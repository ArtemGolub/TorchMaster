using FSM;
using UnityEngine;

public class Grabed_State : State
{
    private Item _item;
    public Grabed_State(Item item)
    {
        _item = item;
    }
    public override void Update()
    {
        Debug.Log(_item.Name + "In wait list");
    }
}