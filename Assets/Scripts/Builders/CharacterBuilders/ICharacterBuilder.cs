using UnityEngine;

public interface ICharacterBuilder
{
    Character GetCharacter();
    void SetComponents(CharacterComponents components);
    void SetCommandManager();
    void SetName(string name);
    void SetSpeed(float speed);
    void SetAttackType(AttackType type);
    void SetMovement(MovementType type);
    void SetFSM(FSMType type);
    void SetInventory(InventoryType type, int capacity);
    void SetAmmoType(AmmoType type);
    void SetCollisions(CollisionTags[] tags);
    void SetFireObserver();
    void SetOilObserver();
    void SetAttackRange(float range);
    void SetRaloadTime(float time);
    void SetMadness(float maxMadness);
    void SetMadnessCommandManager();
    void SetCharacterType(CharacterType type);
    void SetAnimationEventHandler(AnimationEventHandler animationEventHandler);
    void SetAttackAudio(AudioSource audioSource);
}