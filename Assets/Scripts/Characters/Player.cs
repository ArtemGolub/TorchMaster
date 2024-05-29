using System;
using Unity.VisualScripting;
using UnityEngine;

public sealed class Player : ACharacter, IInitialize, IPause
{
    public bool isInit { get; set; }
    public void Init()
    {
        InitCharacter();
        
        Character.SM.InitBehaviour();
        
      //  Character.CommandManager.SubscribeCommand(CharacterCommandType.Attack);
        
        InvokeRepeating("UpdateTorch", 0, 0.1f);
      //  InvokeRepeating("CheckTargets", 0, 0.1f);

        FindObjectOfType<CamraControll>().InitCamera(transform);
        
        Character.MadnessCommandManager.SubscribeCommand(CharacterCommandType.ReduceMadness);

        isInit = true;
    }
    
    public bool isPause { get; set; }
    public void Pause()
    {
        if (isPause)
        {
            InvokeRepeating("UpdateTorch", 0, 0.1f);
            isPause = false;
        }
        else
        {
            CancelInvoke("UpdateTorch");
            isPause = true;
        }
    }
    
    private void Update()
    {
        if(!isInit) return;
        if(isPause) return;
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