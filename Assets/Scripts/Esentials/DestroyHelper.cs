using UnityEngine;

public static class DestroyHelper
{
    public static void Destroy(Object obj)
    {
        // Проверяем, является ли объект MonoBehaviour
        if (obj is MonoBehaviour)
        {
            MonoBehaviour monoBehaviour = obj as MonoBehaviour;
            UnityEngine.Object.Destroy(monoBehaviour.gameObject);
        }
        else
        {
            UnityEngine.Object.Destroy(obj);
        }
    }
}