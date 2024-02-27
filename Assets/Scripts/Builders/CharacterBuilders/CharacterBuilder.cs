using UnityEngine;

public class CharacterBuilder : MonoBehaviour
{
    public static CharacterBuilder current;
    private CharacterDirector _characterDirector;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
        if (current != this)
        {
            Destroy(transform);
        }
    }

    public Character CreateCharacter(Transform transform, Transform invetoryPose, CharacterSO characterData)
    {
        _characterDirector = new CharacterDirector();
        Character character = _characterDirector.CreateCharacter(transform,invetoryPose,characterData);
        return character;
    }
}