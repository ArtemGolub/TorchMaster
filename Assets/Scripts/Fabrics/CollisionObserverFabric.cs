using Unity.VisualScripting;
using UnityEngine;

public static class CollisionObserverFabric
{
    public static void CreateCollisionObserver(Character character, CollisionTags tag)
    {
        switch (tag)
        {
            case CollisionTags.Player:
            {
                break;
            }
            case CollisionTags.Enemy:
            {
                CollisionObserver<IEnemy> observer =
                    character.Components.characterTransform.GetComponent<EnemyCollisionObserver>();
                if (observer == null)
                {
                    observer = character.Components.characterTransform.AddComponent<EnemyCollisionObserver>();
                }

                var collisionHandler = new EnemyCollisionHandler(character);
                observer.AddCollisionHandler(CollisionTagsConverter(tag), collisionHandler);
                break;
            }
            case CollisionTags.Item:
            {
                CollisionObserver<IItem> observer =
                    character.Components.characterTransform.GetComponent<ItemCollisionObserver>();
                if (observer == null)
                {
                    observer = character.Components.characterTransform.AddComponent<ItemCollisionObserver>();
                }

                var collisionHandler = new ItemCollisionHandler(character);
                observer.AddCollisionHandler(CollisionTagsConverter(tag), collisionHandler);
                break;
            }
        }
    }

    private static string CollisionTagsConverter(CollisionTags tag)
    {
        switch (tag)
        {
            case CollisionTags.Player:
            {
                return "Player";
            }
            case CollisionTags.Enemy:
            {
                return "Enemy";
            }
            case CollisionTags.Item:
            {
                return "Item";
            }
            default:
            {
                Debug.LogError("No CollisionTag: " + tag + " implementation");
                return "";
            }
        }
    }
}