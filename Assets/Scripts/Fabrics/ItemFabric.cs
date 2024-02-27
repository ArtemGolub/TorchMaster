using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFabric : MonoBehaviour
{
    public static ItemFabric current;
    
    [SerializeField]private List<Transform> spawnPoints;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
        if (current != this)
        {
            Destroy(transform);
        }
    }

    public void SpawnAllItems(ItemSO preset)
    {
        foreach (var spawnPoint in spawnPoints)
        {
            SpawnItem(preset, spawnPoint);
        }
    }
    
    public Transform SpawnItem(ItemSO preset, Transform spawnPose)
    {
        switch (preset.itemType)
        {
            case ItemType.Torch:
            {
                var obj = Instantiate(preset.prefab, spawnPose.position, spawnPose.rotation);
                return obj;
            }
        }
        return null;
    }
}
