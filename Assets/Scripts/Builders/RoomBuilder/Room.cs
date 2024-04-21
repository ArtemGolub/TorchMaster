using System.Collections.Generic;
using UnityEngine;

public sealed class Room : MonoBehaviour
{
    [Header("Room Content Setup")] 
    private RoomContent _roomContent;
    [SerializeField] private RoomSO preset;


    private Dictionary<Transform, bool> spawnPoints = new Dictionary<Transform, bool>();
    
    [SerializeField] private List<Transform> ItemSpawnPoints;
    [SerializeField] private List<Transform> EnemySpawnPoints;
    [HideInInspector]public List<Transform> PatrolPoints;
    private void Start()
    {
        PatrolPoints.AddRange(ItemSpawnPoints);
        PatrolPoints.AddRange(EnemySpawnPoints);
        
        _roomContent = RoomContentGenerator.current.SetContent(preset, ItemSpawnPoints, EnemySpawnPoints, spawnPoints);
        RoomContentGenerator.current.CreateContent(_roomContent);
    }
}
