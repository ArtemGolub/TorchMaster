using FSM;
using Movement;

public class PlayerWin_State : State
{
    private Character _character;

    public PlayerWin_State(Character character)
    {
        _character = character;
    }

    public override void Enter()
    {
        TorchCanvas.current.DeactivateSlider();
        MadnessCanvas.current.DeactivateSlider();
        JoystickMovementController.current.DeactivateCanvas();

        _character.CommandManager.UnSubscribeCommand(CharacterCommandType.Move);
        _character.CommandManager.UnSubscribeCommand(CharacterCommandType.Attack);
        
        _character.Components.characterTransform.gameObject.SetActive(false);
    }
}