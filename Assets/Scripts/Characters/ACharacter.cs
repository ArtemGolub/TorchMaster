using UnityEngine;

public abstract class ACharacter : MonoBehaviour
{
    [SerializeField] protected CharacterSO CharacterPreset;
    public Character Character;
    public Transform inventoryTransform;

    protected void InitCharacter()
    {
        Character = CharBuilder.current.CreateCharacter(CharacterPreset, transform, inventoryTransform);
    }

    protected void UnSubscribeAll()
    {
        Character.CommandManager.UnSubscribeAll();
    }
}