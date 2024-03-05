
    using UnityEngine;

    public static class InstanceHelper
    {
        public static GameObject InstantiatePrefab(string prefabName, Vector3 position, Quaternion rotation)
        {
            GameObject prefab = Resources.Load<GameObject>(prefabName);
            if (prefab != null)
            {
                return Object.Instantiate(prefab, position, rotation);
            }
            else
            {
                Debug.LogError("Failed to load prefab: " + prefabName);
                return null;
            }
        }
    }
