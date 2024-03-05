
    using UnityEngine;

    public class ComponentBuilder: IComponentBuilder
    {
        private CharacterComponents _components = new CharacterComponents();
        
        public void SetCharacterTransform(Transform transform)
        {
            _components.characterTransform = transform;
        }

        public void SetInventoryTransform(Transform transform)
        {
            _components.inventoryTransform = transform;
        }

        public CharacterComponents GetComponents()
        {
            return _components;
        }
    }
