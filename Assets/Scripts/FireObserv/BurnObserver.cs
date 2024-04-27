using UnityEngine;

public class BurnObserver : AObserver<IBurnStategy>
{
    public static BurnObserver current;
    
    private void Awake()
    {
        current = this;
    }

    private void FixedUpdate()
    {
        if (observers == null)
        {
            TorchCanvas.current.DeactivateSlider();
            return;
        }
        Burn();
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