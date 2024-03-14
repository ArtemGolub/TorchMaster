using System;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{
    public enum RotationType
    {
        OnlyRotation,
        TwoRotations,
        FourRotations
    }
    
    [Serializable]
    public class RoomDirection
    {
        public Direction Direction;
        public DirectionType[] DirectionTypes;

        public RoomDirection(RoomDirection direction)
        {
            if(direction == null) return;
            Direction = direction.Direction;
            DirectionTypes = direction.DirectionTypes;
        }
    }

    public List<RoomDirection> Directions;
    [Range(1, 100)]
    public int Weight = 50;

    public RotationType Rotation;
    public Vector2Int gridPosition;


    public void RotateRoom90()
    {
        transform.Rotate(0,90,0);
        
        RoomDirection dirForward = new RoomDirection(null);
        RoomDirection dirBackward = new RoomDirection(null);
        RoomDirection dirRight = new RoomDirection(null);
        RoomDirection dirLeft = new RoomDirection(null);
        
        foreach (var direction in Directions)
        {
            if (direction.Direction == Direction.Forward)
            {
                dirForward = new RoomDirection(direction);
            }
            else if (direction.Direction == Direction.Backward)
            {
                dirBackward = new RoomDirection(direction);
            }
            else if (direction.Direction == Direction.Right)
            {
                dirRight = new RoomDirection(direction);
            }
            else if (direction.Direction == Direction.Left)
            {
                dirLeft = new RoomDirection(direction);
            }
        }
        
        foreach (var direction in Directions)
        {
            if (direction.Direction == Direction.Backward)
            {
                direction.DirectionTypes = dirRight.DirectionTypes;
            }
            else if (direction.Direction == Direction.Forward)
            {
                direction.DirectionTypes = dirLeft.DirectionTypes;
            }
            else if (direction.Direction == Direction.Left)
            {
                direction.DirectionTypes = dirBackward.DirectionTypes;
            }
            else if(direction.Direction == Direction.Right)
            {
                direction.DirectionTypes = dirForward.DirectionTypes;
            }
        }
    }
}
