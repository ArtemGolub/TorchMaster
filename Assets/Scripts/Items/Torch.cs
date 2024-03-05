public sealed class Torch : AItem
{
    private void Awake()
    {
        InitItem();
    }
    private void Start()
    {
        item.FSM.InitBehaviour();
    }
    
    private void Update()
    {
        item.FSM.UpdateBehaviour();
    }
}