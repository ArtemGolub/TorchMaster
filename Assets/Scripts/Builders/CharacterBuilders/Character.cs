using FSM;
using Movement;


public class Character
{
    public string Name;
    public int ID;

    public float Speed;
    
    public CharacterType CharacterType;
    public IMovementStategy MovementType;
    public ICharacterStateMachine SM;
    public IInventory Inventory;


}