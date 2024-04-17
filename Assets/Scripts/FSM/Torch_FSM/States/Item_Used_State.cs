using FSM;

public class Item_Used_State : State
{
    private Item _item;
    public Item_Used_State(Item item)
    {
        _item = item;
    }

    public override void Enter()
    {
        DestroyHelper.Destroy(_item.Transform.gameObject);
    }
}