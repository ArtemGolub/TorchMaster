using Unity.VisualScripting;
using UnityEngine;

public sealed class Player : ACharacter
{
    private void Awake()
    {
        InitCharacter();
        InitCollisionObserver();
    }

    private void Start()
    {
        Character.SM.InitBehaviour();
    }

    private void Update()
    {
        Character.SM.UpdateBehaviour();
    }

    protected override void InitCollisionObserver()
    {
        EnemyCollisionObserver();
        ItemCollisionObserver();
    }

    private void EnemyCollisionObserver()
    {
        var enemyCollisionObserver = Character.Components.characterTransform.AddComponent<EnemyCollisionObserver>();
        var EnemyCollisionHandler = new EnemyCollisionHandler(Character);
        enemyCollisionObserver.AddCollisionHandler("Enemy",EnemyCollisionHandler);
    }

    private void ItemCollisionObserver()
    {
        var itemCollisionObserver = Character.Components.characterTransform.AddComponent<ItemCollisionObserver>();
        var ItemCollisionHandler = new ItemCollisionHandler(Character);
        itemCollisionObserver.AddCollisionHandler("Item",ItemCollisionHandler);
    }
}