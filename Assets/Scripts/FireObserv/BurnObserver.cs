using UnityEngine;

public class BurnObserver : AObserver<IBurnStategy>, IInitialize, IPause
{
    public static BurnObserver current;
    
    public bool isInit { get; set; }
    public void Init()
    {
        isInit = true;
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
    private void Awake()
    {
        current = this;
    }

    private void FixedUpdate()
    {
        if(!isInit) return;
        if(isPause) return;
        if (observers == null)
        {
            TorchCanvas.current.DeactivateSlider();
            return;
        }
        Burn();
    }

    public void ReduceBurn(float value)
    {
        for (int i = 0; i < observers.Count; i++)
        {
            if (observers[i] == null) continue;
            observers[i].ReduceBurn(value);
        }
    }
    private void Burn()
    {
        for (int i = 0; i < observers.Count; i++)
        {
            if (observers[i] == null) continue;
            observers[i].Burn();
        }
    }

}