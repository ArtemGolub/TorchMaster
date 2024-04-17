using System.Collections.Generic;
using UnityEngine;

public sealed class Room : MonoBehaviour
{
    [Header("Room Content Setup")] 
    private RoomContent _roomContent;
    [SerializeField] private RoomSO preset;
    [SerializeField] private List<Transform> ItemSpawnPoints;
    [SerializeField] private List<Transform> EnemySpawnPoints;
    public List<Transform> PatrolPoints;
    private void Start()
    {
        PatrolPoints.AddRange(ItemSpawnPoints);
        PatrolPoints.AddRange(EnemySpawnPoints);
        
        _roomContent = RoomContentGenerator.current.SetContent(preset, ItemSpawnPoints, EnemySpawnPoints);
        RoomContentGenerator.current.CreateContent(_roomContent);
    }
}
