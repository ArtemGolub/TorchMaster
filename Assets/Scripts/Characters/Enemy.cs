using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private CharacterSO characterPreset;
    [SerializeField] private Transform inventoryPose;
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
        //Character = Builder.current.CreateCharacter(transform, inventoryPose, characterPreset);
    }
}
