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
            if(observer.StateCondition(CharacterStateType.Fear)) return;
            if(observer.StateCondition(CharacterStateType.Attack)) return;
            if(observer.StateCondition(CharacterStateType.Use)) return;
            observer.ChancgeState(CharacterStateType.Move);
        }
    }

    public void IdleState()
    {
        foreach (var observer in observers)
        {
            if(observer.StateCondition(CharacterStateType.Fear)) return;
            if(observer.StateCondition(CharacterStateType.Attack)) return;
            if(observer.StateCondition(CharacterStateType.Use)) return;
            observer.ChancgeState(CharacterStateType.Idle);
        }
    }
}