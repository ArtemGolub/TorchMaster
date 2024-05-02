using FSM;
using UnityEngine;

public class Death_State : State
{
    private Character _character;
    public Death_State(Character character)
    {
        _character = character;
    }

    public override void Enter()
    {
        if (_character.Components.animator != null)
        {
            _character.Components.animator.SetBool("isDead", true);
        }
        
        _character.InventoryCommandManager.ExecuteCommand(CharacterCommandType.Throw);
        TorchCanvas.current.DeactivateSlider();
        MadnessCanvas.current.DeactivateSlider();
        
        DestroyHelper.Destroy(_character.Components.characterTransform.gameObject);
        RestartButton.current.OpenRestartCanvas();
    }
}
