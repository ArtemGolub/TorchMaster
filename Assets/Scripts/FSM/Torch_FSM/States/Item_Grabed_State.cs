using FSM;
using UnityEngine;

public class Item_Grabed_State : State
{
    private Item _item;
    public Item_Grabed_State(Item item)
    {
        _item = item;
    }

    public override void Enter()
    {
        if (_item.ItemType == ItemType.Torch)
        {
            foreach (Transform lightPoint in  _item.LightPoint)
            {
                lightPoint.transform.gameObject.SetActive(false);
            }
        }
    }
}