
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
            collidedObject._Character.SM.ChancgeState(CharacterStateType.Fired); // Change to Disapear or smthing
        }
    }
