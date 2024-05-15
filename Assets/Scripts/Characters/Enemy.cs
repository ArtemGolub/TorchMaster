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

    private void OnDestroy()
    {
        Destroy();
    }
    
    // TODO Refactor
    public Character _Character
    {
        get
        {
            return Character;
        }
    }

    public bool canSeePlayer { get; set; }
}
