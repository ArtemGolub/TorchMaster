
    using UnityEngine;

    public interface IItemStateMachine
    {
        public void InitBehaviour();
        public void UpdateBehaviour();
        
        public void Grab(IInventory inventory);
        public void Active();
        public void Removed();
        public void Seek(Transform target);
    }
