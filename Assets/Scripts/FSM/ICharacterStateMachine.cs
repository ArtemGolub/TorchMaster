namespace FSM
{
    public interface ICharacterStateMachine
    {
        public void InitBehaviour();
        public void UpdateBehaviour();
        public void ChancgeState(CharacterStateType characterStateType);
    }
}