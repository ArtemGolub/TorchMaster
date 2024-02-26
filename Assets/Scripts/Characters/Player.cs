using System;
using UnityEngine;

public sealed class Player : MonoBehaviour
{
    [SerializeField] private CharacterSO characterPreset;
    public Character Character;

    private void Awake()
    {
        InitCharacter();
    }

    private void Start()
    {
        Character.SM.InitBehaviour();
    }

    private void Update()
    {
        Character.SM.UpdateBehaviour();
    }

    private void InitCharacter()
    {
        Character = CharacterBuilder.current.CreateCharacter(transform, characterPreset);
    }
}