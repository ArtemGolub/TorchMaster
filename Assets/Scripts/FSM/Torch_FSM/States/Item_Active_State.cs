using FSM;
public class Item_Active_State : State
{
    private Item _item;
    public Item_Active_State(Item item)
    {
        _item = item;
    }
    public override void Enter()
    {
        _item.ItemCommandManager.SubscribeCommand(ItemCommandType.Active);
        _item.Collider.enabled = false;
        _item.Transform.GetComponentInChildren<LightingPoint>().EnableLighting();
    }
    public override void Exit()
    {
        _item.ItemCommandManager.UnSubscribeCommand(ItemCommandType.Active);
    }
}