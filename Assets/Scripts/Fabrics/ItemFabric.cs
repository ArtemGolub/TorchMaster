using System.Collections.Generic;
using UnityEngine;

public class ItemFabric : MonoBehaviour
{
    public static ItemFabric current;
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
    public void SpawnItems(Tile tile, List<Transform> spawnPoints)
    {
        if(tile.PossibleItems == null || spawnPoints == null) return;
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            int randomIndex = Random.Range(0, tile.PossibleItems.Count);
            SpawnItem(tile.PossibleItems[randomIndex], spawnPoints[i]);
        }

    }
    
    // TODO Refactor
    public Transform SpawnItem(ItemSO preset, Transform spawnPose)
    {
        switch (preset.itemType)
        {
            case ItemType.Torch:
            {
                var obj = Instantiate(preset.prefab, spawnPose.position, spawnPose.rotation);
               // obj.SetParent(spawnPose);
                return obj;
            }
            case ItemType.Oil:
            {
                var obj = Instantiate(preset.prefab, spawnPose.position, spawnPose.rotation);
                //obj.SetParent(spawnPose);
                return obj;
            }
            case ItemType.TrueSight:
            {
                var obj = Instantiate(preset.prefab, spawnPose.position, spawnPose.rotation);
                //obj.SetParent(spawnPose);
                return obj;
            }
            case ItemType.Key:
            {
                var obj = Instantiate(preset.prefab, spawnPose.position, spawnPose.rotation);
                //obj.SetParent(spawnPose);
                return obj;
            }
        }
        return null;
    }
}
