
    using UnityEngine;

    public interface IComponentBuilder
    {
        public void SetCharacterTransform(Transform transform);
        public void SetInventoryTransform(Transform transform);
        public CharacterComponents GetComponents();
    }
