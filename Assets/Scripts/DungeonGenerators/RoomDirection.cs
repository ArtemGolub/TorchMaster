using System;

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
