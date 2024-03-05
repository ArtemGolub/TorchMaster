
    using UnityEngine;

    public class EnemyCollisionHandler: ICollisionHandler<Enemy>
    {
        readonly Character _character;

        public EnemyCollisionHandler(Character character)
        {
            _character = character;
        }
        
        public void HandleCollision(Enemy collidedObject)
        {
            Debug.Log("Enemy collision");
            //_character.CommandManager.SubscribeCommand(CommandType.Attack);
        }
    }
