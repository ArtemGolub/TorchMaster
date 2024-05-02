using UnityEngine;

public class EncreaseMadness: IMadnessCommand
{
    private Character _character;

    public EncreaseMadness(Character character)
    {
        _character = character;
    }
    
    public void Execute()
    {
        Encrease(_character);
    }

    public void ExecuteValue(float value)
    {
        EncreaseValue(_character, value);
    }

    private void Encrease(Character character)
    {
        if (character.curMadness < character.maxMadness)
        {
            character.curMadness += Time.deltaTime;
            MadnessCanvas.current.UpdateSlider(character.curMadness);
        }
        else
        {
            Debug.Log("Implement Death");
        }
    }
    
    private void EncreaseValue(Character character, float value)
    {
        character.curMadness += value;
        if (character.curMadness > character.maxMadness)
        {
            character.curMadness = character.maxMadness;
        }
        MadnessCanvas.current.UpdateSlider(character.curMadness);
    }
}