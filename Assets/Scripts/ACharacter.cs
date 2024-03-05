using Unity.VisualScripting;
using UnityEngine;

public abstract class ACharacter: MonoBehaviour
{
      [SerializeField] protected CharacterSO CharacterPreset;
      public Character Character;
      public Transform inventoryTransform;
      
      protected void InitCharacter()
      {
            Character = CharBuilder.current.CreateCharacter(CharacterPreset, transform, inventoryTransform);
            
            Character.InventoryCommandManager.InitInventory(5,5 );

           
      }
      
      protected abstract void InitCollisionObserver();
}