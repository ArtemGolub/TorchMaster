using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]private CharacterSO player;

    private void Start()
    {
        GameStart();
    }

    private void GameStart()
    {
       CharacterFabric.current.SpawnCharacter(player);
    }
}