
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
            _character.SM.ChancgeState(CharacterStateType.Fear);
            collidedObject._Character.SM.ChancgeState(CharacterStateType.Attack);
            collidedObject._Character.Components.characterTransform.LookAt(_character.Components.characterTransform);
        }
    }
