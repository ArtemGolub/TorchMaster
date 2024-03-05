using FSM;
using UnityEngine;

public class Fired_State : State
{
    private Character _character;

    public Fired_State(Character character)
    {
        _character = character;
    }

    public override void Enter()
    {
        _character.CommandManager.UnSubscribeCommand(CharacterCommandType.Move);

        DestroyHelper.Destroy(_character.Components.characterTransform.gameObject);
    }
}