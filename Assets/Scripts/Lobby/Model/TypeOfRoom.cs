using System;

public enum TypeOfRoom
{
    PvP,
    AllVsAll
}

public static class TypeOfRoomExtensions
{
    public static int ToNumberOfPlayers(this TypeOfRoom state)
    {
        return state switch
        {
            TypeOfRoom.PvP => 2,
            TypeOfRoom.AllVsAll => 4,
            _ => 0
        };
    }
    public static TypeOfRoom ToTypeOfRoom(this int specificInt)
    {
        return specificInt switch
        {
            2 => TypeOfRoom.PvP,
            4 => TypeOfRoom.AllVsAll,
            _ => throw new ArgumentOutOfRangeException(nameof(specificInt), specificInt, null)
        };
    }
}

