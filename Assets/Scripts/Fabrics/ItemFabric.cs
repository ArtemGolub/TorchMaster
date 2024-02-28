using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemFabric : MonoBehaviour
{
    public static ItemFabric current;
    
   [SerializeField]private List<Transform> _spawnPoints;

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

    public void AddSpawnPoints(List<Transform> spawnPoints)
    {
        foreach (var spawnPoint in spawnPoints)
        {
            _spawnPoints.Add(spawnPoint);
        }
    }

    public void SpawnAllItems(ItemSO preset)
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            SpawnItem(preset, spawnPoint);
        }
    }

    public void SpawnItems(Tile tile, List<Transform> spawnPoints)
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            int randomIndex = Random.Range(0, tile.PossibleItems.Count);
            SpawnItem(tile.PossibleItems[randomIndex], spawnPoints[i]);
        }

    }
    
    public Transform SpawnItem(ItemSO preset, Transform spawnPose)
    {
        switch (preset.itemType)
        {
            case ItemType.Torch:
            {
                var obj = Instantiate(preset.prefab, spawnPose.position, spawnPose.rotation);
                obj.SetParent(spawnPose);
                return obj;
            }
            case ItemType.Oil:
            {
                var obj = Instantiate(preset.prefab, spawnPose.position, spawnPose.rotation);
                obj.SetParent(spawnPose);
                return obj;
            }
        }
        return null;
    }
}
