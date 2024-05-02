
    public interface ICollisionHandler<T>
    {
        void HandleCollision(T collidedObject);
        void HandleCollisionExit(T collidedObject);
    }
