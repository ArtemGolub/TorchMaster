using FSM;
using Movement;

public class Player
{
    private readonly StateMachine _sm;
    private readonly IMovable _movementComponent;
    
    private Player(StateMachine fsm, IMovable movementComponent)
    {
        _sm = fsm;
        _movementComponent = movementComponent;
    }
    
    public void Update()
    {
        if (_sm == null) return;
        if(_sm.CurrentState == null) return;
        _sm.UpdateState();
    }
    
    public class Builder
    {
        private StateMachine _sm;
        private IMovable _movementComponent;
    
    
        public Builder SetFSM(StateMachine sm)
        {
            _sm = sm;
            return this;
        }
    
        public Builder SetMovementComponent(IMovable movementComponent)
        {
            _movementComponent = movementComponent;
            return this;
        }
    
        public Player Build()
        {
            if (_sm == null || _movementComponent == null)
            {
                throw new System.Exception("FSM and MovementComponent must be set before building the player.");
            }

            return new Player(_sm, _movementComponent);
        }
    }
}
