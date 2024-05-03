using FSM;
using UnityEngine;

public class KeyGrab_State : State
{
    private Item _item;

    public KeyGrab_State(Item item)
    {
        _item = item;
    }

    public override void Enter()
    {
        if (_item.Transform == null) return;
        // TODO Refactor
        var Character =  GameObject.FindObjectOfType<Player>().Character;
        Character.CommandManager.ExecuteCommand(CharacterCommandType.Use, Character);
        DestroyHelper.Destroy(_item.Transform.gameObject);
    }
}