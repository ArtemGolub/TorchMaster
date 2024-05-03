
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
            //TODO reduce value in collidedObject
            _character.MadnessCommandManager.ExecuteCommand(CharacterCommandType.ReduceMadness, 25);
            
            collidedObject._Character.SM.ChancgeState(CharacterStateType.Attack);
            collidedObject._Character.Components.characterTransform.LookAt(_character.Components.characterTransform);
        }

        public void HandleCollisionExit(IEnemy collidedObject)
        {
            
        }

        public void HandleCollisionStay(IEnemy collidedObject)
        {
          
        }
    }
