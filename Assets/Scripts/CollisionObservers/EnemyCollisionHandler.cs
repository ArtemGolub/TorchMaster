
    using UnityEngine;

    public class EnemyCollisionHandler: ICollisionHandler<IEnemy>
    {
        readonly Character _character;

        public EnemyCollisionHandler(Character character)
        {
            _character = character;
        }
        
        public void HandleCollision(IEnemy collidedObject)
        {
            Debug.Log("Enemy collision");
            //_character.CommandManager.SubscribeCommand(CommandType.Attack);
        }
    }
