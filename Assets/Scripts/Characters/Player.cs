using System;

public sealed class Player : ACharacter
{
    private void Awake()
    {
        InitCharacter();
    }

    private void Start()
    {
        Character.SM.InitBehaviour();
        
        Character.CommandManager.SubscribeCommand(CharacterCommandType.Attack);
        
        InvokeRepeating("UpdateTorch", 0, 0.1f);
        InvokeRepeating("CheckTargets", 0, 0.1f);

        FindObjectOfType<CamraControll>().InitCamera(transform);
    }

    private void Update()
    {
        Character.SM.UpdateBehaviour();
    }


    private void UpdateTorch()
    {
        Character.TorchObserver.CheckBurningObjects(Character);
    }

    private void CheckTargets()
    {
        Character.OilObserver.CheckAvaliableOil();
    }

    private void OnDestroy()
    {
        Destroy();
    }
}