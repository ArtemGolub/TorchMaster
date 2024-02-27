using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]private CharacterSO player;
    [SerializeField] private ItemSO item;

    private void Start()
    {
        GameStart();
    }

    private void GameStart()
    {
       CharacterFabric.current.SpawnCharacter(player);
       ItemFabric.current.SpawnAllItems(item);
    }
}