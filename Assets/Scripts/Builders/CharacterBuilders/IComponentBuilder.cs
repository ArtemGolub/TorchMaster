
    using UnityEngine;

    public interface IComponentBuilder
    {
        public void SetCharacterTransform(Transform transform);
        public void SetInventoryTransform(Transform transform);
        public void SetAnimator(Animator animator);
        public CharacterComponents GetComponents();
    }
