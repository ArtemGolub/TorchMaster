using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CharacterSO player;
    [SerializeField] private CharacterSO enemy;

    private void Start()
    {
        GameStart();
    }

    private void GameStart()
    {
        CharacterFabric.current.SpawnCharacter(player);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            CharacterFabric.current.SpawnCharacter(enemy);
        }
    }
}