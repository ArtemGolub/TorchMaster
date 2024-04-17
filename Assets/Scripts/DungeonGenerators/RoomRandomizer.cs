using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public static class RoomRandomizer
{
    public static DungeonRoom GetRandomRoom(List<DungeonRoom> availableRooms)
    {
        List<float> chances = new List<float>();
        for (int i = 0; i < availableRooms.Count; i++)
        {
            chances.Add(availableRooms[i].Weight);
        }

        float value = Random.Range(0, chances.Sum());
        float sum = 0;

        for (int i = 0; i < chances.Count; i++)
        {
            sum += chances[i];
            if (value < sum)
            {
                return availableRooms[i];
            }
        }
        
        return availableRooms[availableRooms.Count - 1];
    }
}