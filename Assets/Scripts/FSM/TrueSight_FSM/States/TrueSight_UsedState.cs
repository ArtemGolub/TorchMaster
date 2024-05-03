using FSM;
using UnityEngine;

public class TrueSight_UsedState : State
{
    private Item _item;

    public TrueSight_UsedState(Item item)
    {
        _item = item;
    }

    public override void Enter()
    {
        if (_item.Transform == null) return;
        GameObject.FindObjectOfType<Player>().Character.MadnessCommandManager.ExecuteCommand(CharacterCommandType.EncreaseMadnessValue, 25);
        DestroyHelper.Destroy(_item.Transform.gameObject);
    }
}