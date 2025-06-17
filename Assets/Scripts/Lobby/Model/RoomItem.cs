public class RoomItem
{
    public string RoomName;
    public int ActualPlayers;
    public TypeOfRoom TypeOfRoom;

    public int NumberOfPlayers()
    {
        switch (TypeOfRoom)
        {
            case TypeOfRoom.PvP:
                return 2;
            case TypeOfRoom.AllVsAll:
                return 4;
            default:
                return 999;
        }
    }

}