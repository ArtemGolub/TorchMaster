using UnityEngine;

public class CharacterDirector
{
    private ICharacterBuilder _characterBuilder;
    
    public Character CreateCharacter(Transform transform,CharacterSO characterData)
    {
        _characterBuilder = CharacterBuilderFabric.CharacterBuilder(characterData.characterType);
        
        SetParams(characterData);
        SetBehaviour(transform, characterData);
        
        Character character =  _characterBuilder.GetCharacter();
        return character;
    }

    private void SetParams(CharacterSO characterData)
    {
        _characterBuilder.SetName(characterData.Name);
        _characterBuilder.SetID(characterData.iD);

        _characterBuilder.SetSpeed(characterData.speed);
    }

    private void SetBehaviour(Transform transform, CharacterSO characterData)
    {
        _characterBuilder.SetMovement(transform, characterData.movementType);
        _characterBuilder.SetFSM(transform, characterData.fsmType);
    }
    
}
