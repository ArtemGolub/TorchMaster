
public class Enemy : ACharacter, IEnemy
{
    private void Awake()
    {
        InitCharacter();
    }

    private void Start()
    {
        Character.SM.InitBehaviour();
    }

    private void Update()
    {
        Character.SM.UpdateBehaviour();
    }
    
}
