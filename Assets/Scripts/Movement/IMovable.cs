using UnityEngine;

namespace Movement
{
    public interface IMovable
    {
        void OnMove(Vector3 direction);
    }
}

