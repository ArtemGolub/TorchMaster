public class MadnessObserver: AObserver<IMadnessCommand>
{
    public static MadnessObserver current;

    private void Awake()
    {
        current = this;
    }

    private void FixedUpdate()
    {
        if (observers == null)
        {
            return;
        }
        MadnessDo();
    }

    private void MadnessDo()
    {
        for (int i = 0; i < observers.Count; i++)
        {
            if (observers[i] == null) continue;
            observers[i].Execute();
        }
    }
}