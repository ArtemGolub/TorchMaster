using Movement;
using UnityEngine;

public class PlayerBuilder : MonoBehaviour
{
    [SerializeField] private JoystickMovementController joystickMovementController;
    
    private Player _player;
    
    private IMovable _movementComponent;
    private Player_SM _sm;
    private void Start()
    {
        BuildComponents();
        BuildPlayer();
    }
    private void Update()
    {
        _player.Update();
    }

    private void BuildComponents()
    {
        _movementComponent = new MovementComponent(transform, 2f);
        _sm = new Player_SM(_movementComponent, joystickMovementController);
    }
    private void BuildPlayer()
    {
        _player = new Player.Builder()
            .SetMovementComponent(_movementComponent)
            .SetFSM(_sm)
            .Build();
    }
}
