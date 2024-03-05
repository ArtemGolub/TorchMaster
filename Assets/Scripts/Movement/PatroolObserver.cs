public class PatroolObserver : AObserver<IPatrolStrategy>
{
    public static PatroolObserver current;

    private void Awake()
    {
        current = this;
    }

    private void FixedUpdate()
    {  
        if(observers == null) return;
        Patrool();
    }

    private void Patrool()
    {
        for (int i = 0; i < observers.Count; i++)
        {
            if(observers[i] == null) return;
            observers[i].Patrol();
        }
    }
}