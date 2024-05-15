using UnityEngine;

public class CharBuilder : MonoBehaviour
{
    public static CharBuilder current;
    private ICharacterBuilder _characterBuilder;
    private IComponentBuilder _componentBuilder;

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
    
    public Character CreateCharacter(CharacterSO characterData, Transform characterTransform, Transform inventoryTransform, Animator animator)
    {
        _characterBuilder = CharacterBuilderFabric.CharacterBuilder(characterData.characterType);
        
        _characterBuilder.SetComponents(SetComponents(characterTransform, inventoryTransform, animator));

        if (characterData.characterType == CharacterType.Player)
        {
            _characterBuilder.SetAnimationEventHandler(animator.transform.GetComponent<AnimationEventHandler>());
        }
        
        SetParams(characterData);
        SetBehaviour(characterData);
        
        Character character =  _characterBuilder.GetCharacter();
        return character;
    }

    private CharacterComponents SetComponents(Transform characterTransform, Transform inventoryTransform,
        Animator animator)
    {
        _componentBuilder = new ComponentBuilder();
        
        _componentBuilder.SetCharacterTransform(characterTransform);
        _componentBuilder.SetInventoryTransform(inventoryTransform);
        _componentBuilder.SetAnimator(animator);
        
        CharacterComponents components = _componentBuilder.GetComponents();
        return components;
    }
    
    private void SetParams(CharacterSO characterData)
    {
        _characterBuilder.SetName(characterData.Name);
        _characterBuilder.SetSpeed(characterData.speed);
        _characterBuilder.SetAttackRange(characterData.attackRange);
        _characterBuilder.SetRaloadTime(characterData.raloadTime);
        _characterBuilder.SetCharacterType(characterData.characterType);
    }

    private void SetBehaviour(CharacterSO characterData)
    {
        _characterBuilder.SetCommandManager();
        _characterBuilder.SetMovement(characterData.movementType);
        _characterBuilder.SetAmmoType(characterData.ammoType);
        _characterBuilder.SetAttackType(characterData.attackType);
        _characterBuilder.SetCollisions(characterData.collisionTags);
        
        if (characterData.characterType == CharacterType.Player)
        {
            _characterBuilder.SetInventory(characterData.inventoryType, characterData.TorchCapacity);
            _characterBuilder.SetFireObserver();
            _characterBuilder.SetOilObserver();
            
            _characterBuilder.SetMadness(characterData.maxMadness);
            _characterBuilder.SetMadnessCommandManager();
        }
        _characterBuilder.SetFSM(characterData.fsmType);
    }


}