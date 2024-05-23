using UnityEngine;

public abstract class ACharacter : MonoBehaviour
{
    [SerializeField] protected CharacterSO CharacterPreset;
    [SerializeField] protected Animator _animator;
    public Character Character;
    public Transform inventoryTransform;

    protected void InitCharacter()
    {
        Character = CharBuilder.current.CreateCharacter(CharacterPreset, transform, inventoryTransform, _animator);

        var attackSound = Instantiate(Character.attackAudio, Character.Components.characterTransform);
        Character.attackAudio = attackSound;
    }

    protected void Destroy()
    {
        Character.CommandManager.UnSubscribeAll();
        Destroy(Character.Components.characterTransform.gameObject);
    }
}