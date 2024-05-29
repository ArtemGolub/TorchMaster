public class MadnessObserver: AObserver<IMadnessCommand>, IInitialize, IPause
{
    public static MadnessObserver current;
    public bool isInit { get; set; }
    public void Init()
    {
        isInit = true;
    }
    private void Awake()
    {
        current = this;
    }
    public bool isPause { get; set; }
    public void Pause()
    {
        if (isPause)
        {
            isPause = false;
        }
        else
        {
            isPause = true;
        }
    }
    private void FixedUpdate()
    {
        if(!isInit) return;
        if(isPause) return;
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