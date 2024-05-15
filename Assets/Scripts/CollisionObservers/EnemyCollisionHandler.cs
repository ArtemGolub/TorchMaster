
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
            if (!TryAttack(collidedObject))
            {
                _character.SM.ChancgeState(CharacterStateType.Fear);
                //TODO reduce value in collidedObject
                _character.MadnessCommandManager.ExecuteCommand(CharacterCommandType.ReduceMadness, 25);
                collidedObject._Character.SM.ChancgeState(CharacterStateType.Attack);
                
                Vector3 direction = collidedObject._Character.Components.characterTransform.position - _character.Components.characterTransform.position;
                direction.y = 0;
                Quaternion rotation = Quaternion.LookRotation(direction);
                collidedObject._Character.Components.characterTransform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
               // collidedObject._Character.Components.characterTransform.LookAt(_character.Components.characterTransform);
            }
        }

        private bool TryAttack(IEnemy collidedObject)
        {
            if(collidedObject._Character.Name != "GhostFollow") return false;
            if (collidedObject.canSeePlayer) return false;;
            if(_character.InventoryCommandManager.GetItem(ItemType.Torch) == null) return false;;
            
            _character.SM.ChancgeState(CharacterStateType.Attack);
            collidedObject._Character.SM.ChancgeState(CharacterStateType.Death);
            BurnObserver.current.ReduceBurn(5);
            return true;
        }
        
        public void HandleCollisionExit(IEnemy collidedObject)
        {
            
        }

        public void HandleCollisionStay(IEnemy collidedObject)
        {
          
        }
    }
