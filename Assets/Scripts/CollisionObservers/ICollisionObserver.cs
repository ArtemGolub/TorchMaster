
    public interface ICollisionObserver<T>
    {
        void AddCollisionHandler(string tag, ICollisionHandler<T> handler);
    }
