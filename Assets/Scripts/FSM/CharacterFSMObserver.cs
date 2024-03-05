using FSM;

public class CharacterFSMObserver : AObserver<ICharacterStateMachine>
{
    public static CharacterFSMObserver current;
    
    private void Awake()
    {
        current = this;
    }

    public void MoveState()
    {
        foreach (var observer in observers)
        {
            observer.ChancgeState(CharacterStateType.Move);
        }
    }

    public void IdleState()
    {
        foreach (var observer in observers)
        {
            observer.ChancgeState(CharacterStateType.Idle);
        }
    }
}