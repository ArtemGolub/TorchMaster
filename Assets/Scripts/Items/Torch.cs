public sealed class Torch : AItem
{
    private void Awake()
    {
        
    }
    private void Start()
    {
        InitItem();
        item.FSM.InitBehaviour();
    }
    
    private void Update()
    {
        item.FSM.UpdateBehaviour();
    }
}