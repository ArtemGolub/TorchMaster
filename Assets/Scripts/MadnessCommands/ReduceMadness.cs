using UnityEngine;

public class ReduceMadness : IMadnessCommand
{
    private Character _character;

    public ReduceMadness(Character character)
    {
        _character = character;
    }

    public void Execute()
    {
        Reduce(_character);
    }

    public void ExecuteValue(float value)
    {
        ReduceValue(_character, value);
    }

    private void Reduce(Character character)
    {
        if (character.curMadness > 0)
        {
            character.curMadness -= 10 * Time.deltaTime;
            MadnessCanvas.current.UpdateSlider(character.curMadness);
        }
        else
        {
            character.SM.ChancgeState(CharacterStateType.Death);
        }
    }

    private void ReduceValue(Character character, float value)
    {
        character.curMadness -= value;
        MadnessCanvas.current.UpdateSlider(character.curMadness);
        if (character.curMadness <= 0)
        {
            character.SM.ChancgeState(CharacterStateType.Death);
        }
    }
}